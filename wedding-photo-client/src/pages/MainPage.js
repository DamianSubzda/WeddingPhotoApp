import React, { useState, useRef } from "react";
import Header from "./../components/header/Header";
import Section from "./../components/common/Section";
import Button from "./../components/photoUpload/Button";
import "./MainPage.css";
import PhotoViewer from "./../components/photoUpload/PhotoViewer";
import GalleryHeader from "./../components/gallery/GalleryHeader";
import Gallery from "./../components/gallery/Gallery";
import { BsFillXCircleFill } from "react-icons/bs";
import { FaRotateRight } from "react-icons/fa6";
import apiClient from "./../services/apiClient";

function MainPage() {
  const [photo, setPhoto] = useState(null);
  const [photoFile, setPhotoFile] = useState(null);
  const [rotation, setRotation] = useState(0);
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
      setRotation(0);
    }
  };

  const sendPhoto = async () => {
    if (!photoFile) {
      alert("Proszę najpierw wybrać zdjęcie!");
      return;
    }

    try {
      const data = await apiClient.uploadPhoto(photoFile, rotation, true);
      console.log("Success:", data);
      alert("Zdjęcie zostało przesłane!");
      deletePhoto();
      setReloadGallery((prev) => !prev);
    } catch (error) {
      alert("Wystąpił błąd podczas przesyłania zdjęcia!");
    }
  };

  const deletePhoto = () => {
    setPhoto(null);
    setPhotoFile(null);
    setRotation(0);
  };

  const rotatePhoto = () => {
    setRotation((prevRotation) => prevRotation + 90);
  };

  return (
    <div className="main-page">
      <Header className="header"></Header>
      <div className="content">
        <Section>
          {photo && (
            <div className="viewer">
              <BsFillXCircleFill className="delete-button" onClick={deletePhoto} />
              <FaRotateRight className="rotate-button" onClick={rotatePhoto} />
              <PhotoViewer src={photo} rotation={rotation} />
            </div>
          )}
          <div className="button-group">
            <input
              type="file"
              accept="image/*"
              capture="environment"
              onChange={handleFileChange}
              style={{ display: "none" }}
              ref={fileInputRef}
            />
            <Button content="Zrób zdjęcie" onClick={() => fileInputRef.current.click()} />
            <input
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              style={{ display: "none" }}
              ref={galleryInputRef}
            />
            <Button content="Dodaj z galerii" onClick={() => galleryInputRef.current.click()} />
            {photo && <Button content="Prześlij" onClick={sendPhoto} />}
          </div>
        </Section>
        <Section>
          <GalleryHeader />
          <Gallery reloadGallery={reloadGallery} />
        </Section>
      </div>
    </div>
  );
}

export default MainPage;
