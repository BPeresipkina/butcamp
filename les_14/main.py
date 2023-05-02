import cv2

face_cascades = cv2.CascadeClassifier(cv2.data.haarcascades + "haarcascade_frontalface_default.xml")

img = cv2.imread('face.jpeg')
img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

faces = face_cascades.detectMultiScale(img_gray)

for(x, y, w, h) in faces:
    cv2.rectangle(img, (x, y), (x + w, y + h), (0, 0, 255), 2)

# img = cv2.imread('cat.jpg')
# print(img.shape)

# img = cv2.resize(img, (500, 500))
# print(img.shape)

cv2.imshow('Resalt', img)

cv2.waitKey(0)
