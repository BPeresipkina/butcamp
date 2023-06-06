from flask import Flask, render_template
from random import randint as rd

app = Flask(__name__)

@app.route('/')
def main():
    # name = rd(0, 1)
    # return render_template('base.html', name=name)
    with open('file.txt', 'r', encoding='utf-8') as file:
        resultData = list()
        for line in file.readlines():
            resultData.append(tuple(line.split('\n')[0].split(';')))

    return render_template('base.html', data=resultData)

@app.route('/about')
def about():
    return render_template('about.html')

if __name__ == '__main__':
    app.run()
