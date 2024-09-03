import InfiniteScroll from 'react-infinite-scroll-component';
import './Gallery.css';
import axios from 'axios';
import { React, useEffect, useState } from 'react';
import Photo from './Photo';

function Gallery({reloadGallery}) {
    const pageSize = 10;
    const [photos, setPhotos] = useState([]);
    const [hasMore, setHasMore] = useState(true);
    const [pageNumber, setPageNumber] = useState(2);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        axios
            .get(`https://localhost:7058/api/photos?pageNumber=1&pageSize=${pageSize}`)
            .then((res) => {
                setPhotos(res.data);
                setHasMore(res.data.length >= pageSize);
            })
            .catch((err) => console.log(err))
            .finally(() => setIsLoading(false));
    }, [reloadGallery]);

    const fetchPhotos = () => {
        axios
            .get(`https://localhost:7058/api/photos?pageNumber=${pageNumber}&pageSize=${pageSize}`)
            .then((response) => {
                setPhotos((prevPhotos) => [...prevPhotos, ...response.data]);
                setHasMore(response.data.length >= pageSize);
            })
            .catch((err) => console.log(err));

        setPageNumber((prevPageNumber) => prevPageNumber + 1);
    };

    return (
        <div className='gallery'>
            {isLoading ? (
                <h4>Loading...</h4>
            ) : (
                photos.length === 0 ? (
                    <p style={{ textAlign: 'center' }}>
                        <b>Brak zdjęć do wyświetlenia</b>
                    </p>
                ) : (
                    <InfiniteScroll
                        style={{ width: 'inherit' }}  
                        dataLength={photos.length}
                        next={fetchPhotos}
                        hasMore={hasMore}
                        loader={<h4>Loading...</h4>}
                        endMessage={
                            <p style={{ textAlign: 'center' }}>
                                <b>Nie ma więcej... Niestety!</b>
                            </p>
                        }
                    >
                        {photos
                        .filter(photo => photo.addToGallery)
                        .map(photo => (
                            <Photo key={photo.id} photo={photo} />
                        ))}
                    </InfiniteScroll>
                )
            )}
        </div>
    );
}

export default Gallery;
