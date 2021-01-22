import sys
from pdf2image import convert_from_path
pages = convert_from_path(sys.argv[1], 500)

for pageIndex, page in enumerate(pages):
    page.save('C:\\ProgramData\\FormXtractor\\Documents\\Jpg\\' + str(pageIndex) + '.jpg', 'JPEG')