import numpy as np
from tensorflow.keras.models import Sequential
from DataGenerator import DataGenerator
from tensorflow.keras.layers import *
from tensorflow.keras.models import Model

# Generators
training_generator = DataGenerator(r'C:/Users/Andre/Desktop/lab2/crt-trn/', n_channels=4)


from PIL import Image

input_shape=(64,64,4)


input_img = Input(shape=input_shape)  # adapt this if using `channels_first` image data format
x = Conv2D(32, (3, 3), activation='relu', padding='same')(input_img)
x = MaxPooling2D((2, 2), padding='same')(x)
x = Conv2D(32, (3, 3), activation='relu', padding='same')(x)
encoded = MaxPooling2D((2, 2), padding='same')(x)
x = Conv2D(32, (3, 3), activation='relu', padding='same')(encoded)
x = UpSampling2D((2, 2))(x)
x = Conv2D(32, (3, 3), activation='relu', padding='same')(x)
x = UpSampling2D((2, 2))(x)
decoded = Conv2D(4, (3, 3), activation='sigmoid', padding='same')(x)
autoencoder = Model(input_img, decoded)
autoencoder.compile(optimizer='adam', loss='MSE')

# Train model on dataset
autoencoder.fit_generator(generator=training_generator, epochs=10)