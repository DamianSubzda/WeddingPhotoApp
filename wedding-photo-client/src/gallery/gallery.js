import InfiniteScroll from 'react-infinite-scroll-component';
import './gallery.css';
import axios from 'axios';
import { React, useEffect, useState } from 'react';
import Photo from './photo';

function Gallery({reloadGallery}) {
    const pageSize = 10
    const [photos, setPhotos] = useState([]);
    const [hasMore, setHasMore] = useState(true);
    const [pageNumber, setPageNumber] = useState(2);

    useEffect(()=> {
        axios
        .get(`https://localhost:7058/api/photos?pageNumber=1&pageSize=${pageSize}`)
        .then((res) => setPhotos(res.data))
        .catch((err) => console.log(err));
    }, [reloadGallery]);

    const fetchPhotos = () => {
      axios
        .get(`https://localhost:7058/api/photos?pageNumber=${pageNumber}&pageSize=${pageSize}`)
        .then((response) => {
          setPhotos((prevPhotos) => [...prevPhotos, ...response.data]);
          response.data.length < pageSize ? setHasMore(false) : setHasMore(true);
        })
        .catch((err) => console.log(err));

        setPageNumber((prevPageNumber) => prevPageNumber + 1);
    };


    return (
        <div className='gallery'>
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
                }>
                {photos
                .filter(photo => photo.addToGallery)
                .map(photo => (
                    <Photo key={photo.id} photo={photo} />
                ))}
            </InfiniteScroll>
        </div>
    );
}

export default Gallery;