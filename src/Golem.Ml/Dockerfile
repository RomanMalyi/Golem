FROM tiangolo/python-machine-learning:python3.7

WORKDIR /usr/src/app

COPY ["Golem.Ml/main.py", "./"]
COPY ["Golem.Ml/glove.6B.100d.txt.word2vec", "./"]

RUN pip install --upgrade gensim
RUN pip install flask

CMD ["python", "./main.py"]