import React, { useState, useRef } from "react";
import Header from "./header/Header";
import Section from "./Section";
import Button from "./photoUpload/button";
import "./app.css";
import PhotoViewer from "./photoUpload/photoViewer";
import GalleryHeader from "./gallery/galleryHeader";
import Gallery from "./gallery/gallery";
import { BsFillXCircleFill } from "react-icons/bs";

function App() {
  const [photo, setPhoto] = useState(null);
  const [photoFile, setPhotoFile] = useState(null);
  const fileInputRef = useRef(null);
  const galleryInputRef = useRef(null);
  const [reloadGallery, setReloadGallery] = useState(false);

  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setPhoto(reader.result);
      };
      reader.readAsDataURL(file);
      setPhotoFile(file);
    }
  };

  const sendPhoto = () => {
    if (!photoFile) {
      alert("Proszę najpierw wybrać zdjęcie!");
      return;
    }
    const formData = new FormData();
    formData.append("file", photoFile);

    fetch("https://localhost:7058/api/photos/upload", {
      method: "POST",
      body: formData,
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Success:", data);
        alert("Zdjęcie zostało przesłane!");
        deletePhoto();
        setReloadGallery((prev) => !prev);
      })
      .catch((error) => {
        console.error("Error:", error);
        alert("Wystąpił błąd podczas przesyłania zdjęcia!");
      });
  };

  const deletePhoto = () => {
    setPhoto(null);
    setPhotoFile(null);
  };

  return (
    <div className="App">
      <Header></Header>
      <div className="content">
        <Section>
          {photo && (
            <div className="viewer">
              <BsFillXCircleFill className="delete-button" onClick={deletePhoto} />
              <PhotoViewer src={photo} />
            </div>)}
          <div className="button-group">
            <input
              type="file"
              accept="image/*"
              capture="environment"
              onChange={handleFileChange}
              style={{ display: "none" }}
              ref={fileInputRef}
            />
            <Button
              content="Zrób zdjęcie"
              onClick={() => fileInputRef.current.click()}
            />
            <input
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              style={{ display: "none" }}
              ref={galleryInputRef}
            />
            <Button
              content="Dodaj z galerii"
              onClick={() => galleryInputRef.current.click()}
            />
            <Button content="Prześlij" onClick={sendPhoto} />
          </div>
        </Section>
        <Section>
          <GalleryHeader></GalleryHeader>
          <Gallery reloadGallery={reloadGallery} />
        </Section>
      </div>
    </div>
  );
}

export default App;
