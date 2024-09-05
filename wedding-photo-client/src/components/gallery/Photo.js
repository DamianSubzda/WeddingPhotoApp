import { useState } from "react";
import "./Photo.css";
import CloseButton from "../common/CloseButton";

function Photo({ photo }) {
  const [isFullscreen, setIsFullscreen] = useState(false);

  const formattedDate = new Date(photo.createdAt).toLocaleDateString("en-GB", {
    day: "2-digit",
    month: "2-digit",
  });

  const formattedTime = new Date(photo.createdAt).toLocaleTimeString("en-GB", {
    hour: "2-digit",
    minute: "2-digit",
  });

  const hidePhoto = () => {
    setIsFullscreen(false);
  };

  const showPhoto = () => {
    setIsFullscreen(true);
  };

  return (
    <>
      <div className="photo-container">
        <img
          className="photo"
          src={photo.fullFilePath}
          alt={`${photo.fileName}`}
          onClick={showPhoto}
        />
        <div className="photo-caption">
          <p className="photo-time">
            {formattedDate} {formattedTime}
          </p>
          <p className="photo-description">{photo.description}</p>
        </div>
      </div>
      {isFullscreen &&
      (
        <div className="full-screen" onClick={hidePhoto}>
          <CloseButton className="hide-button" onClick={hidePhoto} />
          <img
            src={photo.fullFilePath}
            alt={`${photo.fileName}`}
            onClick={(e) => e.stopPropagation()}
          />
        </div>
      )}
    </>
  );
}

export default Photo;
