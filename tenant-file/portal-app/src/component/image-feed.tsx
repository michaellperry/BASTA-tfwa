import React, { useEffect, useState } from 'react';
import styled from 'styled-components';

const StyledImageFeed = styled.div`
  display: grid;
  gap: 0.5rem;
  width: 90%;
  max-width: 1600px;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  justify-content: center;
  margin: 0 auto;

  .image_container {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    overflow: hidden;
    max-height: 140px;
  }
`;

// Type for sample data from Lorem Picsum, can be scrapped later
interface Image {
  id: string;
  author: string;
  width: number;
  height: number;
  url: string;
  download_url: string;
}

const ImageFeed = () => {
  // TODO: correct type - when pulling from back end, set type to string[]
  const [images, setImages] = useState<Image[]>([]);
  const [sortField, setSortField] = useState('');
  const [sortOrder, setSortOrder] = useState(1);
  useEffect(() => {
    async function getImages() {
      const blob = await fetch('https://picsum.photos/v2/list');
      const result = await blob.json();
      console.log(result);
      setImages(result);
    }
    getImages();
  }, []);

  function sortImages(field: keyof Image, order: number) {
    // Correct types here when incorperating images from back end
    const sortedImages = [...images].sort((a: Image, b: Image) => {
      if (a[field] > b[field]) {
        return 1;
      } else if (a[field] < b[field]) {
        return -1;
      } else {
        return 0;
      }
    });
    setImages(sortedImages);
  }

  return (
    <StyledImageFeed>
      <button onClick={() => sortImages('author', 1)}>Sort</button>
      {images.map((image) => {
        return (
          <div className="image_container">
            <img src={image.download_url} alt="" style={{ width: '100%' }} />
          </div>
        );
      })}
    </StyledImageFeed>
  );
};

export default ImageFeed;
