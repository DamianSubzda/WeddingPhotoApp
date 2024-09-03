import React, { useRef, useEffect, useState } from "react";
import "./PhotoViewer.css";
import { BsFillXCircleFill } from "react-icons/bs";
import { FaRotateRight } from "react-icons/fa6";

function PhotoViewer({ photo, rotation, deletePhoto, rotatePhoto, descriptionInputRef }) {
  const imageRef = useRef(null);
  const [divImageStyles, setDivImageStyles] = useState({});
  const [imageStyles, setImageStyles] = useState({});

  useEffect(() => {
    const img = imageRef.current;
    if (img) {
      const updateImageStyles = () => {
        const { naturalWidth, naturalHeight } = img;
        const aspectRatio = naturalWidth / naturalHeight;
        setDivImageStyles({
          height: rotation % 180 === 0 ? "auto" : `${80 * aspectRatio}vw`,
        });
        setImageStyles({
          width: rotation % 180 === 0 ? "80vw" : "auto",
          height: rotation % 180 === 0 ? "auto" : "80vw",
        });
      };

      if (img.complete) {
        updateImageStyles();
      } else {
        img.onload = updateImageStyles;
      }
    }
  }, [photo, rotation]);

  return (
    <>
      {photo && (
        <div className="viewer">
          <BsFillXCircleFill className="delete-button" onClick={deletePhoto} />
          <FaRotateRight className="rotate-button" onClick={rotatePhoto} />
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
