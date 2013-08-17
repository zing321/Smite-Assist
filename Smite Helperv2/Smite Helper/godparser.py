class GodParser:
	totalGods=0
	gods=[]
	pantheon=[]
	title=[]
	roles=[]
	free=[]
	capabilities=[]
	pros=[]
	cons=[]
	lore=[]
	health=[]
	healthPerLevel=[]
	mana=[]
	manaPerLevel=[]
	movement=[]
	hp5=[]
	hp5l=[]
	mp5=[]
	mp5l=[]
	attackSpeed=[]
	attackSpeedPerLevel=[]
	mProt=[]
	mProtl=[]
	pProt=[]
	pProtl=[]
	pPowr=[]
	pPowrl=[]
	abilityNamePassive=[]
	abilityDiscPassive=[]
	abilityMiscPassive=[]
	abilityName1=[]
	abilityDisc1=[]
	abilityMisc1=[]
	abilityName2=[]
	abilityDisc2=[]
	abilityMisc2=[]
	abilityName3=[]
	abilityDisc3=[]
	abilityMisc3=[]
	abilityName4=[]
	abilityDisc4=[]
	abilityMisc4=[]
	@staticmethod
	def parseGods(godFile):
		total1=0
		total2=0
		htmFile=open(godFile, 'r')
		for line in htmFile:
			if '<span id="lblGod" class="god">' in line:
				cut1=line.split('<span id="lblGod" class="god">')
				cut2=cut1[1].split('</span>')
				GodParser.gods.append(cut2[0])
				GodParser.totalGods+=1
			elif '<span id="lblPantheon">' in line:
				cut1=line.split('<span id="lblPantheon">')
				cut2=cut1[1].split('</span>')
				GodParser.pantheon.append(cut2[0])
			elif '<span id="lblTitle">' in line:
				cut1=line.split('<span id="lblTitle">')
				cut2=cut1[1].split('</span>')
				if total1<GodParser.totalGods:
					GodParser.title.append(cut2[0])
					total1+=1
			elif '<span id="lblRoles">' in line:
				cut1=line.split('<span id="lblRoles">')
				cut2=cut1[1].split('</span>')
				if total2<GodParser.totalGods:
					GodParser.roles.append(cut2[0])
					total2+=1
			elif '<span id="lblFree">' in line:
				if '<img alt="" src="./Smite Gods_files/notify.png">' in line:
					GodParser.free.append(True)
				else:
					GodParser.free.append(False)
			elif '<span id="lblType">' in line:
				cut1=line.split('<span id="lblType">')
				cut2=cut1[1].split('</span>')
				GodParser.capabilities.append(cut2[0])
			elif '<span id="lblPros">' in line:
				cut1=line.split('<span id="lblPros">')
				cut2=cut1[1].split('</span>')
				GodParser.pros.append(cut2[0])
			elif '<span id="lblCons">' in line:
				cut1=line.split('<span id="lblCons">')
				cut2=cut1[1].split('</span>')
				GodParser.cons.append(cut2[0])
			elif '<span id="lblLore">' in line:
				cut1=line.split('<span id="lblLore">')
				cut2=cut1[1].split('</span>')
				GodParser.lore.append(cut2[0])
			elif '<span id="lblHealth">' in line:
				cut1=line.split('<span id="lblHealth">')
				cut2=cut1[1].split('</span>')
				GodParser.health.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblHealthLevel">')
				GodParser.healthPerLevel.append(cut3[1])
			elif '<span id="lblMana">' in line:
				cut1=line.split('<span id="lblMana">')
				cut2=cut1[1].split('</span>')
				GodParser.mana.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblManaLevel">')
				GodParser.manaPerLevel.append(cut3[1])
			elif '<span id="lblSpeed">' in line:
				cut1=line.split('<span id="lblSpeed">')
				cut2=cut1[1].split('</span>')
				GodParser.movement.append(cut2[0])
			elif '<span id="lblHealthPerFive">' in line:
				cut1=line.split('<span id="lblHealthPerFive">')
				cut2=cut1[1].split('</span>')
				GodParser.hp5.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblHp5Level">')
				GodParser.hp5l.append(cut3[1])
			elif '<span id="lblManaPerFive">' in line:
				cut1=line.split('<span id="lblManaPerFive">')
				cut2=cut1[1].split('</span>')
				GodParser.mp5.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblMp5Level">')
				GodParser.mp5l.append(cut3[1])
			elif '<span id="lblAttackSpeed">' in line:
				cut1=line.split('<span id="lblAttackSpeed">')
				cut2=cut1[1].split('</span>')
				GodParser.attackSpeed.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblAttackSpeedLevel">')
				GodParser.attackSpeedPerLevel.append(cut3[1])
			elif '<span id="lblMagicProtection">' in line:
				cut1=line.split('<span id="lblMagicProtection">')
				cut2=cut1[1].split('</span>')
				GodParser.mProt.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblMagicProtectionLevel">')
				GodParser.mProtl.append(cut3[1])
			elif '<span id="lblPhysicalProtection">' in line:
				cut1=line.split('<span id="lblPhysicalProtection">')
				cut2=cut1[1].split('</span>')
				GodParser.pProt.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblPhysicalProtectionLevel">')
				GodParser.pProtl.append(cut3[1])
			elif '<span id="lblPhysicalPower">' in line:
				cut1=line.split('<span id="lblPhysicalPower">')
				cut2=cut1[1].split('</span>')
				GodParser.pPowr.append(cut2[0])
				cut3=cut2[1].split('&nbsp;<span id="lblPhysicalPowerLevel">')
				GodParser.pPowrl.append(cut3[1])
			elif 'lblAbility5\" class=\"abilityName\"' in line:
				cut1=line.split('class="abilityName">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityNamePassive.append(cut2[0])
			elif 'lblDescription5' in line:
				cut1=line.split('lblDescription5">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityDiscPassive.append(cut2[0])
				line=htmFile.next()
				if '<div class="abilityDetails">' in line:
					outputString="";
					for x in range(0,22):
						line=htmFile.next()
						cut1=line.split('>')
						cut2=cut1[1].split('<')
						if cut2[0] != '':
							outputString+=cut2[0]+";"
					GodParser.abilityMiscPassive.append(outputString)
			elif 'lblAbility1\" class=\"abilityName\"' in line:
				cut1=line.split('class="abilityName">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityName1.append(cut2[0])
			elif 'lblDescription1' in line:
				cut1=line.split('lblDescription1">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityDisc1.append(cut2[0])
				line=htmFile.next()
				if '<div class="abilityDetails">' in line:
					outputString="";
					for x in range(0,22):
						line=htmFile.next()
						cut1=line.split('>')
						cut2=cut1[1].split('<')
						if cut2[0] != '':
							outputString+=cut2[0]+";"
					GodParser.abilityMisc1.append(outputString)
			elif 'lblAbility2" class="abilityName"' in line:
				cut1=line.split('class="abilityName">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityName2.append(cut2[0])
			elif 'lblDescription2' in line:
				cut1=line.split('lblDescription2">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityDisc2.append(cut2[0])
				line=htmFile.next()
				if '<div class="abilityDetails">' in line:
					outputString="";
					for x in range(0,22):
						line=htmFile.next()
						cut1=line.split('>')
						cut2=cut1[1].split('<')
						if cut2[0] != '':
							outputString+=cut2[0]+";"
					GodParser.abilityMisc2.append(outputString)
			elif 'lblAbility3" class="abilityName"' in line:
				cut1=line.split('class="abilityName">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityName3.append(cut2[0])
			elif 'lblDescription3' in line:
				cut1=line.split('lblDescription3">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityDisc3.append(cut2[0])
				line=htmFile.next()
				if '<div class="abilityDetails">' in line:
					outputString="";
					for x in range(0,22):
						line=htmFile.next()
						cut1=line.split('>')
						cut2=cut1[1].split('<')
						if cut2[0] != '':
							outputString+=cut2[0]+";"
					GodParser.abilityMisc3.append(outputString)
			elif 'lblAbility4" class="abilityName"' in line:
				cut1=line.split('class="abilityName">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityName4.append(cut2[0])
			elif 'lblDescription4' in line:
				cut1=line.split('lblDescription4">')
				cut2=cut1[1].split('</span>')
				GodParser.abilityDisc4.append(cut2[0])
				line=htmFile.next()
				if '<div class="abilityDetails">' in line:
					outputString="";
					for x in range(0,22):
						line=htmFile.next()
						cut1=line.split('>')
						cut2=cut1[1].split('<')
						if cut2[0] != '':
							outputString+=cut2[0]+";"
					GodParser.abilityMisc4.append(outputString)
		htmFile.close()
	@staticmethod
	def Save():
		fSave=open("parsedData\\gods.txt",'w')
		fSave.write(str(GodParser.totalGods)+'\n')
		for x in GodParser.gods:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pantheon.txt",'w')
		for x in GodParser.pantheon:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\title.txt",'w')
		for x in GodParser.title:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\roles.txt",'w')
		for x in GodParser.roles:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\free.txt",'w')
		for x in GodParser.free:
			fSave.write(str(x)+'\n')
		fSave.close()
		fSave=open("parsedData\\capabilities.txt",'w')
		for x in GodParser.capabilities:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pros.txt",'w')
		for x in GodParser.pros:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\cons.txt",'w')
		for x in GodParser.cons:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\lore.txt",'w')
		for x in GodParser.lore:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\health.txt",'w')
		for x in GodParser.health:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\healthPerLevel.txt",'w')
		for x in GodParser.healthPerLevel:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\mana.txt",'w')
		for x in GodParser.mana:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\manaPerLevel.txt",'w')
		for x in GodParser.manaPerLevel:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\movement.txt",'w')
		for x in GodParser.movement:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\hp5.txt",'w')
		for x in GodParser.hp5:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\hp5l.txt",'w')
		for x in GodParser.hp5l:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\mp5.txt",'w')
		for x in GodParser.mp5:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\mp5l.txt",'w')
		for x in GodParser.mp5l:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\attackSpeed.txt",'w')
		for x in GodParser.attackSpeed:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\attackSpeedPerLevel.txt",'w')
		for x in GodParser.attackSpeedPerLevel:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\mProt.txt",'w')
		for x in GodParser.mProt:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\mProtl.txt",'w')
		for x in GodParser.mProtl:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pPowr.txt",'w')
		for x in GodParser.pPowr:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pPowrl.txt",'w')
		for x in GodParser.pPowrl:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pProt.txt",'w')
		for x in GodParser.pProt:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\pProtl.txt",'w')
		for x in GodParser.pProtl:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityNamePassive.txt",'w')
		for x in GodParser.abilityNamePassive:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityDiscPassive.txt",'w')
		for x in GodParser.abilityDiscPassive:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityMiscPassive.txt",'w')
		for x in GodParser.abilityMiscPassive:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityName1.txt",'w')
		for x in GodParser.abilityName1:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityDisc1.txt",'w')
		for x in GodParser.abilityDisc1:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityMisc1.txt",'w')
		for x in GodParser.abilityMisc1:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityName2.txt",'w')
		for x in GodParser.abilityName2:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityDisc2.txt",'w')
		for x in GodParser.abilityDisc2:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityMisc2.txt",'w')
		for x in GodParser.abilityMisc2:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityName3.txt",'w')
		for x in GodParser.abilityName3:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityDisc3.txt",'w')
		for x in GodParser.abilityDisc3:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityMisc3.txt",'w')
		for x in GodParser.abilityMisc3:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityName4.txt",'w')
		for x in GodParser.abilityName4:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityDisc4.txt",'w')
		for x in GodParser.abilityDisc4:
			fSave.write(x+'\n')
		fSave.close()
		fSave=open("parsedData\\abilityMisc4.txt",'w')
		for x in GodParser.abilityMisc4:
			fSave.write(x+'\n')
		fSave.close()
def main():
	GodParser.parseGods("Smite Gods.htm")
	GodParser.Save()
if __name__ == "__main__":
    main()