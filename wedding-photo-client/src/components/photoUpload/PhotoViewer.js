import React, { useRef, useEffect, useState } from "react";
import "./PhotoViewer.css";

function PhotoViewer({ src, rotation }) {
  const imageRef = useRef(null);
  const [divImageStyles, setDivImageStyles] = useState({});
  const [imageStyles, setImageStyles] = useState({});

  useEffect(() => {
    const img = imageRef.current;
    if (img) {
      const updateImageStyles = () => {
        const { naturalWidth, naturalHeight } = img;
        const aspectRatio = naturalWidth / naturalHeight;
        if (rotation % 180 === 0) {
          setDivImageStyles({
            width: "80vw",
            height: "auto",
          });
          setImageStyles({
            width: "80vw",
            height: "auto",
          });
        } else {
          setDivImageStyles({
            width: "80vw",
            height: `${80 * aspectRatio}vw`,
          });
          setImageStyles({
            width: `auto`,
            height: "80vw",
          });
        }
      };

      if (img.complete) {
        updateImageStyles();
      } else {
        img.onload = updateImageStyles;
      }
    }
  }, [src, rotation]);

  return (
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
          src={src}
          style={{
            ...imageStyles,
            transform: `rotate(${rotation}deg)`,
          }}
        />
      </div>
      <div className="description-viewer">
        <label>Podpis </label>
        <input />
      </div>
    </div>
  );
}

export default PhotoViewer;
