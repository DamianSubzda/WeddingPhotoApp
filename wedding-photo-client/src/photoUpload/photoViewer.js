import React from "react";
import './photoViewer.css'

function PhotoViewer({src, rotation}) {
    return <div className="photo-viewer">
        <img className='image-viewer' alt="podgląd zdjęcia" src={src} style={{ transform: `rotate(${rotation}deg)` }}></img>  
    </div>
}

export default PhotoViewer;