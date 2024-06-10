import "./header.css";
import header_image from "./../assets/image/KD_header.png";
import header from "./../assets/image/Nagłowek.png";

function Header() {
  return (
    <>
      <img
        src={header}
        atl='Nagłowek'
        style={{ width: "100vw" }}
      ></img>
    </>
  );
}

export default Header;
