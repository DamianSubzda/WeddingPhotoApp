import { useState, useRef } from "react";
import apiClient from "../services/apiClient";

const usePhotoManager = (onUploadSuccess) => {
  const [photo, setPhoto] = useState(null);
  const [photoFile, setPhotoFile] = useState(null);
  const [rotation, setRotation] = useState(0);
  const fileInputRef = useRef(null);
  const descriptionInputRef = useRef(null);
  const galleryInputRef = useRef(null);

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
    console.log(descriptionInputRef.current.value);
    if (!photoFile) {
      alert("Proszę najpierw wybrać zdjęcie!");
      return;
    }

    try {
      const data = await apiClient.uploadPhoto(photoFile, rotation, true, descriptionInputRef.current.value);
      console.log("Success:", data);
      alert("Zdjęcie zostało przesłane!");
      deletePhoto();
      if (onUploadSuccess) onUploadSuccess();
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

  return {
    photo,
    rotation,
    fileInputRef,
    descriptionInputRef,
    galleryInputRef,
    handleFileChange,
    sendPhoto,
    deletePhoto,
    rotatePhoto,
  };
};

export default usePhotoManager;
