import React from "react";
import './photoViewer.css'

function PhotoViewer({src}) {
    return <div className="photo-viewer">
        <img className='image-viewer' alt="podgląd zdjęcia" src={src}></img>  
    </div>
}

export default PhotoViewer;