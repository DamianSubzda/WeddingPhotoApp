import "./Header.css";
import header from "./../../assets/images/Nagłowek.png";

function Header() {
  return (
    <>
      <img
        src={header}
        alt='Nagłówek...'
        style={{ width: "100vw" }}
      ></img>
    </>
  );
}

export default Header;
