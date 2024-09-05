import "./RotateButton.scss"

function RotateButton({ onClick  }){
    return(
        <button type="button" className="btn-rotate" onClick={ onClick }>
            <div className="arrow-round"></div>
        </button>
    )
}

export default RotateButton;