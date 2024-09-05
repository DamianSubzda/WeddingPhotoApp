import "./CloseButton.scss"

function CloseButton({ onClick, className = '' }) {
    return (
        <button type="button" className={`btn-close ${className}`} onClick={onClick}>
            <span className="icon-cross"></span>
        </button>
    );
}
export default CloseButton;
