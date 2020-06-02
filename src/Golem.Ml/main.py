from flask import Flask
from flask import request
from gensim.models import KeyedVectors

modelFileName = 'glove.6B.100d.txt.word2vec'
model = KeyedVectors.load_word2vec_format(modelFileName, binary=False)

app = Flask(__name__)

@app.route("/similar")
def most_similar():
    words = request.args.get('words').split()
    result = model.most_similar(positive=words, topn=1)
    return result[0][0];

app.run(host='0.0.0.0', port=80, debug=False)
