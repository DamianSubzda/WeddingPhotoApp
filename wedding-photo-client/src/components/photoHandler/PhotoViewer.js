import React, { useRef, useEffect, useState } from "react";
import "./PhotoViewer.css";
import CloseButton from "./../common/CloseButton"
import RotateButton from "./../common/RotateButton"

function PhotoViewer({ photo, rotation, deletePhoto, rotatePhoto, descriptionInputRef }) {
  const imageRef = useRef(null);
  const [divImageStyles, setDivImageStyles] = useState({});
  const [imageStyles, setImageStyles] = useState({});

  useEffect(() => {
    const img = imageRef.current;
    if (img) {
      const updateImageStyles = () => {
        setImageStyles({
          width: rotation % 180 === 0 ? "80vw" : `auto`,
          height: rotation % 180 === 0 ? "auto" : `80vw`,
          maxWidth: rotation % 180 === 0 ? `calc(700px - 10vw)` : `none`,
          maxHeight: rotation % 180 === 0 ? `none` : `calc(700px - 10vw)`,
        });
      };

      if (img.complete) {
        updateImageStyles();
      } else {
        img.onload = updateImageStyles;
      }
    }
  }, [photo, rotation]);

  useEffect(() => {
    const img = imageRef.current;
    if (img) {
      const updateDivStyles = () => {
        const imageWidth = img.width;
        setDivImageStyles({
          height: rotation % 180 === 0 ? "auto" : `${imageWidth}px`,
        });
      };

      if (img.complete) {
        updateDivStyles();
      } else {
        img.onload = updateDivStyles;
      }
    }
  }, [imageStyles, rotation]);

  return (
    <>
      {photo && (
        <div className="viewer">
          <CloseButton onClick={deletePhoto}/>
          <RotateButton onCLick={rotatePhoto}/>
          <div className="viewer-section">
            <div
              className="photo-viewer"
              style={{
                ...divImageStyles,
              }}
            >
              <img
                ref={imageRef}
                className="image-viewer"
                alt="podgląd zdjęcia"
                src={photo}
                style={{
                  ...imageStyles,
                  transform: `rotate(${rotation}deg)`,
                }}
              />
            </div>
            <div className="description-viewer">
              <label>Podpis </label>
              <input ref={descriptionInputRef} />
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default PhotoViewer;
