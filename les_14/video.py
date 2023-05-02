import cv2

face_cascades = cv2.CascadeClassifier(cv2.data.haarcascades + "haarcascade_frontalface_default.xml")

cap = cv2.VideoCapture(0) # сейчас в скобках путь к вебкамере (0), 
                          # если нужно вставить видео вместо 0 => 'video.mp4' 
                          # помещаем файл с видео в папку и пишем его название

while True:
    success, frame = cap.read()
    # cv2.imshow('camera', frame)
    img_gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    faces = face_cascades.detectMultiScale(img_gray)

    for(x, y, w, h) in faces:
        cv2.rectangle(img_gray, (x, y), (x + w, y + h), (0, 0, 255), 2)

    cv2.imshow('result', frame)

    if cv2.waitKey(1) & 0xff == ord('q'):
        break