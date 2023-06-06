from flask import Flask

app = Flask(__name__)

@app.route('/')
def main():
    return "<h1>Hello, world</h1><br><a href='/index'>перейти на 2-ую страницу</a>"

@app.route('/index/<x>/<y>')
def index(x, y):
    return f'Результат: {float(x) + float(y)}'

if __name__ == '__main__':
    app.run()
    