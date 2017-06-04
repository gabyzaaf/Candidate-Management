#!/bin/python
import sys
from xml.etree import ElementTree as et

def changeTheContent(newvalue):
	tree = et.parse("training.csproj")
	tree.find('/PropertyGroup/TargetFramework').text = newvalue
	tree.write("training.csproj")


if __name__=='__main__':
	changeTheContent(str(sys.argv[1]))
