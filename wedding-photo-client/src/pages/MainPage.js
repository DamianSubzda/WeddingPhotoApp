import React, { useState } from "react";
import Header from "./../components/header/Header";
import Section from "./../components/common/Section";
import PhotoUploader from "./../components/photoHandler/PhotoUploader";
import "./MainPage.css";
import PhotoViewer from "../components/photoHandler/PhotoViewer";
import GalleryHeader from "./../components/gallery/GalleryHeader";
import Gallery from "./../components/gallery/Gallery";
import usePhotoManager from "../hooks/usePhotoManager";

function MainPage() {
  const [reloadGallery, setReloadGallery] = useState(false);

  const {
    photo,
    rotation,
    fileInputRef,
    descriptionInputRef,
    galleryInputRef,
    handleFileChange,
    sendPhoto,
    deletePhoto,
    rotatePhoto,
  } = usePhotoManager(() => setReloadGallery((prev) => !prev));

  return (
    <div className="main-page">
      <Header className="header"></Header>
      <div className="content">
        <Section>
          <PhotoViewer
            photo={photo}
            rotation={rotation}
            deletePhoto={deletePhoto}
            rotatePhoto={rotatePhoto}
            descriptionInputRef={descriptionInputRef}
          />
          <PhotoUploader
            handleFileChange={handleFileChange}
            sendPhoto={sendPhoto}
            fileInputRef={fileInputRef}
            galleryInputRef={galleryInputRef}
            photo={photo}
          />
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
