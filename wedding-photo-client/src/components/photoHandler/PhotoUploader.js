import React from "react";
import Button from "./../common/Button";

function PhotoUploader({
  photo,
  handleFileChange,
  fileInputRef,
  galleryInputRef,
  sendPhoto,
}) {
  return (
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
      {photo && <Button content="Prześlij" onClick={sendPhoto} />}
    </div>
  );
}

export default PhotoUploader;
