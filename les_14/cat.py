import cv2

img = cv2.imread('cat.jpg')
print(img)

img = cv2.resize(img, (500, 500))
print(img.shape)