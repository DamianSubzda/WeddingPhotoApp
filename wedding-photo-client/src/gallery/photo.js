import './photo.css';

function Photo({ photo }) {
    
    const formattedDate = new Date(photo.createdAt).toLocaleDateString('en-GB', {
        day: '2-digit',
        month: '2-digit'
    });

    const formattedTime = new Date(photo.createdAt).toLocaleTimeString('en-GB', {
        hour: '2-digit',
        minute: '2-digit'
    });

    return (
        <div className="photo-container">
            <img className='photo' src={photo.fullFilePath} alt={`${photo.fileName}`} />
            <div className="photo-caption">
                <p className="photo-time">{formattedDate} {formattedTime}</p>
                <p className="photo-description">{photo.description}</p>
            </div>
        </div>
    );
}

export default Photo;
