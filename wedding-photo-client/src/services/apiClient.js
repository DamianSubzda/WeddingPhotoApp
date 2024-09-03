const API_URL = "https://localhost:7058/api/";

const apiClient = {
  uploadPhoto: async (photoFile, rotation, addToGallery) => {
    const formData = new FormData();
    formData.append("file", photoFile);
    formData.append("rotation", rotation);
    formData.append("addToGallery", addToGallery);

    try {
      const response = await fetch(`${API_URL}photos/upload`, {
        method: "POST",
        body: formData,
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  },

  fetchPhotos: async (pageNumber = 1, pageSize = 10) => {
    try {
      const response = await fetch(`${API_URL}photos?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        method: "GET",
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  }
};

export default apiClient;
