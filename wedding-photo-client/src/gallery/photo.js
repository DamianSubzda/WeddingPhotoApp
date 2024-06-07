import './photo.css'

function Photo({ photo }) {
    return (
        <img className='image-gallery' src={photo.fullFilePath} alt={`${photo.fileName}`} />
    );
}


export default Photo;