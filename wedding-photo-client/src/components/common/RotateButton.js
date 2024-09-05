import "./RotateButton.scss"

function RotateButton({ onCLick }){
    return(
        <button className="btn-rotate" onClick={onCLick}>
            <div className="arrow-round"></div>
        </button>
    )
}

export default RotateButton;