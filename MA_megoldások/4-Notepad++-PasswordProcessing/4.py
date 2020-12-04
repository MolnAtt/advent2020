def sorbarendezésCserékkel(s)
  s = re.sub(r'(\w)\n(\w)', r'\1 \2', s) # felesleges sortörések eltüntetése
  s = re.sub(r'(\n\S*\n)', r' \n', s) # üres sorokat gyárt sajnos a python, mert strippelni kéne, de text editorban ilyen nincs. Igazából mindegy, az eredményt nem fogja érinteni.  
  s = re.sub(r"(\n)(.*?)(byr:.*? )", "\n" + r"\1\3\2", s) # byr-eket az elejére
  s = re.sub(r"(\nbyr:.*? )(.*?)(iyr:.*? )", r"\1\3\2", s) # iyr-eket a byr-ek után
  s = re.sub(r"(\n.*?iyr:.*? )(.*?)(eyr:.*? )", r"\1\3\2", s) # eyr-eket a iyr-ek után
  s = re.sub(r"(\n.*?eyr:.*? )(.*?)(hgt:.*? )", r"\1\3\2", s) # hgt-ket a eyr-ek után
  s = re.sub(r"(\n.*?hgt:.*? )(.*?)(hcl:.*? )", r"\1\3\2", s) # hcl-eket a hgt-k után
  s = re.sub(r"(\n.*?hcl:.*? )(.*?)(ecl:.*? )", r"\1\3\2", s) # ecl-eket a hcl-ek után
  s = re.sub(r"(\n.*?ecl:.*? )(.*?)(pid:.*? )", r"\1\3\2", s) # pid-eket a ecl-ek után
  s = re.sub(r"(\n.*?pid:.*? )(.*?)(cid:.*? )", r"\1\3\2", s) # cid-eket a pid-ek után
  return s

def elsofeladat(s):
  s = sorbarendezésCserékkel(s)
  li = re.findall(r'\n(byr:.*? )(iyr:.*? )(eyr:.*? )(hgt:.*? )(hcl:.*? )(ecl:.*? )(pid:.*? )(cid:.*? )?', s)
  print(f"az \"érvényes\" útlevelek száma: {len(li)}")

def masodikfeladat(s):
  s = sorbarendezésCserékkel(s)
  li = re.findall(r'\nbyr:(19[2-9]\d|200[0-2]) iyr:20(1\d|20) eyr:20(2\d|30) hgt:(1([5-8]\d|9[0-3])cm|(59|6\d|7[0-6])in) hcl:(#[0-9a-f]{6}) ecl:(amb|blu|brn|gry|grn|hzl|oth) pid:(\d{9} )( cid:.*?)?', s)
  print(f"az \"érvényes\" útlevelek száma: {len(li)}")