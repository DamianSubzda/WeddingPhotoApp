import InfiniteScroll from "react-infinite-scroll-component";
import "./Gallery.css";
import { useEffect, useState } from "react";
import Photo from "./Photo";
import apiClient from "../../services/apiClient";

function Gallery({ reloadGallery }) {
  const pageSize = 10;
  const [photos, setPhotos] = useState([]);
  const [hasMore, setHasMore] = useState(true);
  const [pageNumber, setPageNumber] = useState(2);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const loadInitialPhotos = async () => {
      try {
        const data = await apiClient.fetchPhotos(1, pageSize);
        setPhotos(data);
        setHasMore(data.length >= pageSize);
      } catch (error) {
        console.log(error);
      } finally {
        setIsLoading(false);
      }
    };

    loadInitialPhotos();
  }, [reloadGallery]);

  const fetchPhotos = async () => {
    try {
      const data = await apiClient.fetchPhotos(pageNumber, pageSize);
      setPhotos((prevPhotos) => [...prevPhotos, ...data]);
      setHasMore(data.length >= pageSize);
      setPageNumber((prevPageNumber) => prevPageNumber + 1);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="gallery">
      {isLoading ? (
        <h4>Loading...</h4>
      ) : photos.length === 0 ? (
        <p style={{ textAlign: "center" }}>
          <b>Brak zdjęć do wyświetlenia</b>
        </p>
      ) : (
        <InfiniteScroll
          style={{ width: "inherit" }}
          dataLength={photos.length}
          next={fetchPhotos}
          hasMore={hasMore}
          loader={<h4>Loading...</h4>}
          endMessage={
            <p style={{ textAlign: "center" }}>
              <b>Nie ma więcej... Niestety!</b>
            </p>
          }
        >
          {photos
            .filter((photo) => photo.addToGallery)
            .map((photo) => (
              <Photo key={photo.id} photo={photo} />
            ))}
        </InfiniteScroll>
      )}
    </div>
  );
}

export default Gallery;
