function CalcTrained(TrainedSavingThrows, TrainedSkills) {

    var SavingThrows = (TrainedSavingThrows >>> 0).toString(2);
    var Skills = (TrainedSkills >>> 0).toString(2);
    
    var SavingThrowArray = [];
    var SkillsArray = [];

    while (SavingThrows.length != 6) {
        SavingThrows = "0" + SavingThrows;
    }
    while (Skills.length != 18) {
        Skills = "0" + Skills;
    }

    var STLength = SavingThrows.length;
    var SkillsLength = Skills.length;

    for (var x = 0; x < STLength; x++) {
        if (SavingThrows.substring(STLength - 1 - x, STLength - x) == 1) {
            SavingThrowArray[STLength - x-1] = true;
        }
        else {
            SavingThrowArray[STLength - x-1] = false;
        }
    }
    for (var x = 0; x < SkillsLength; x++) {
        if(Skills.substring(SkillsLength - 1 - x, SkillsLength - x) == 1){
            SkillsArray[SkillsLength - x-1] = true;
            }
        else {
            SkillsArray[SkillsLength - x-1] = false;
        }
    }
    
    for (var x = 0; x < SkillsLength; x++) {
        if (SkillsArray[x] == 1) {
            switch (x) {
                case 0:
                    document.getElementById('CkAcrobatics').checked = true;
                    break;
                case 1:
                    document.getElementById('CkAnimHand').checked = true;
                    break;
                case 2:
                    document.getElementById('CkArcana').checked = true;
                    break;
                case 3:
                    document.getElementById('CkAthletics').checked = true;
                    break;
                case 4:
                    document.getElementById('CkDeception').checked = true;
                    break;
                case 5:
                    document.getElementById('CkHistory').checked = true;
                    break;
                case 6:
                    document.getElementById('CkInsight').checked = true;
                    break;
                case 7:
                    document.getElementById('CkIntimidation').checked = true;
                    break;
                case 8:
                    document.getElementById('CkInvestigation').checked = true;
                    break;
                case 9:
                    document.getElementById('CkMedicine').checked = true;
                    break;
                case 10:
                    document.getElementById('CkNature').checked = true;
                    break;
                case 11:
                    document.getElementById('CkPerception').checked = true;
                    break;
                case 12:
                    document.getElementById('CkPerformance').checked = true;
                    break;
                case 13:
                    document.getElementById('CkPersuasion').checked = true;
                    break;
                case 14:
                    document.getElementById('CkReligion').checked = true;
                    break;
                case 15:
                    document.getElementById('CkSoH').checked = true;
                    break;
                case 16:
                    document.getElementById('CkStealth').checked = true;
                    break;
                case 17:
                    document.getElementById('CkSurvival').checked = true;
                    break;

            }
        }
    }
    for (var x = 0; x < STLength; x++) {
        if (SavingThrowArray[x] == 1) {
            switch (x) {
                case 0:
                    document.getElementById('StStrCk').checked = true;
                    break;
                case 1:
                    document.getElementById('StDexCk').checked = true;
                    break;
                case 2:
                    document.getElementById('StConCk').checked = true;
                    break;
                case 3:
                    document.getElementById('StIntCk').checked = true;
                    break;
                case 4:
                    document.getElementById('StWisCk').checked = true;
                    break;
                case 5:
                    document.getElementById('StChaCk').checked = true;
                    break;
            }
        }
    }
}

function CalcScores() {
    var ProfBonus = 0;
    var strMod = 0;
    var dexMod = 0;
    var conMod = 0;
    var wisMod = 0;
    var intMod = 0;
    var chaMod = 0;
    
    //get the ProfBonus
    ProfBonus = document.getElementById('ProfBonus').value;

    //Calculate modifiers
    var CurScore = document.getElementById('ScStr').value;
    var CurMod = 0;
    for (var x = 0; x <= 5; x++) {
        
        if (+CurScore > 30) {
            CurMod = 10;
        }
        else if (+CurScore < 0) {
            CurMod = -5
        }
        else {
            switch (+CurScore) {
                case 0:
                case 1:
                    CurMod = -5;
                    break;
                case 2:
                case 3:
                    CurMod = -4;
                    break;
                case 4:
                case 5:
                    CurMod = -3;
                    break;
                case 6:
                case 7:
                    CurMod = -2;
                    break;
                case 8:
                case 9:
                    CurMod = -1;
                    break;
                case 10:
                case 11:
                    CurMod = 0;
                    break;
                case 12:
                case 13:
                    CurMod = 1;
                    break;
                case 14:
                case 15:
                    CurMod = 2;
                    break;
                case 16:
                case 17:
                    CurMod = 3;
                    break;
                case 18:
                case 19:
                    CurMod = 4;
                    break;
                case 20:
                case 21:
                    CurMod = 5;
                    break;
                case 22:
                case 23:
                    CurMod = 6;
                    break;
                case 24:
                case 25:
                    CurMod = 7;
                    break;
                case 26:
                case 27:
                    CurMod = 8;
                    break;
                case 28:
                case 29:
                    CurMod = 9;
                    break;
                case 30:
                case (value > 30):
                    CurMod = 10;
                    break;
            }
        }
        switch (x) {
            case 0:
                strMod = CurMod;
                CurScore = document.getElementById('ScDex').value
                break;
            case 1:
                dexMod = CurMod;
                CurScore = document.getElementById('ScCon').value
                break;
            case 2:
                ConMod = CurMod;
                CurScore = document.getElementById('ScWis').value
                break;
            case 3:
                wisMod = CurMod;
                CurScore = document.getElementById('ScInt').value
                break;
            case 4:
                intMod = CurMod;
                CurScore = document.getElementById('ScCha').value
                break;
            case 5:
                chaMod = CurMod;
                break;

        }

    }
    //Saving throw scores

    //Str
    var score = 0;
    if (document.getElementById('StStrCk').checked == true) {
        score += +ProfBonus;
    }
    score += +strMod;
    document.getElementById('StStr').innerHTML = " " + score;

    //Dex
    score = 0;
    if (document.getElementById('StDexCk').checked == true) {
        score += +ProfBonus;
    }
    score += +dexMod;
    document.getElementById('StDex').innerHTML = " " + score;

    //Con
    score = 0;
    if (document.getElementById('StConCk').checked == true) {
        score += +ProfBonus;
    }
    score += +ConMod;
    document.getElementById('StCon').innerHTML = " " + score;

    //Wis
    score = 0;
    if (document.getElementById('StWisCk').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('StWis').innerHTML = " " + score;

    //Int
    score = 0;
    if (document.getElementById('StIntCk').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('StInt').innerHTML = " " + score;

    //Cha
    score = 0;
    if (document.getElementById('StChaCk').checked == true) {
        score += +ProfBonus;
    }
    score += +chaMod;
    document.getElementById('StCha').innerHTML = " " + score;

    //Skills

    //Acrobatics
    score = 0;
    if (document.getElementById('CkAcrobatics').checked == true) {
        score += +ProfBonus;
    }
    score += +dexMod;
    document.getElementById('Acrobatics').innerHTML = " " + score;

    //Animal Handling
    score = 0;
    if (document.getElementById('CkAnimHand').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('AnimHand').innerHTML = " " + score;

    //Arcana
    score = 0;
    if (document.getElementById('CkArcana').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('Arcana').innerHTML = " " + score;
   

    //Athletics
    score = 0;
    if (document.getElementById('CkAthletics').checked == true) {
        score += +ProfBonus;
    }
    score += +strMod;
    document.getElementById('Athletics').innerHTML = " " + score;

    //Deception
    score = 0;
    if (document.getElementById('CkDeception').checked == true) {
        score += +ProfBonus;
    }
    score += +chaMod;
    document.getElementById('Deception').innerHTML = " " + score;

    //History
    score = 0;
    if (document.getElementById('CkHistory').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('History').innerHTML = " " + score;

    //Insight
    score = 0;
    if (document.getElementById('CkInsight').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('Insight').innerHTML = " " + score;

    //Intimidation
    score = 0;
    if (document.getElementById('CkIntimidation').checked == true) {
        score += +ProfBonus;
    }
    score += +chaMod;
    document.getElementById('Intimidation').innerHTML = " " + score;

    //Investigation
    score = 0;
    if (document.getElementById('CkInvestigation').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('Investigation').innerHTML = " " + score;

    //Medicine
    score = 0;
    if (document.getElementById('CkMedicine').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('Medicine').innerHTML = " " + score;

    //Nature
    score = 0;
    if (document.getElementById('CkNature').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('Nature').innerHTML = " " + score;

    //Perception
    score = 0;
    if (document.getElementById('CkPerception').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('Perception').innerHTML = " " + score;

    //Performance
    score = 0;
    if (document.getElementById('CkPerformance').checked == true) {
        score += +ProfBonus;
    }
    score += +chaMod;
    document.getElementById('Performance').innerHTML = " " + score;

    //Persuasion
    score = 0;
    if (document.getElementById('CkPersuasion').checked == true) {
        score += +ProfBonus;
    }
    score += +chaMod;
    document.getElementById('Persuasion').innerHTML = " " + score;

    //Religion
    score = 0;
    if (document.getElementById('CkReligion').checked == true) {
        score += +ProfBonus;
    }
    score += +intMod;
    document.getElementById('Religion').innerHTML = " " + score;

    //Sleight of hand
    score = 0;
    if (document.getElementById('CkSoH').checked == true) {
        score += +ProfBonus;
    }
    score += +dexMod;
    document.getElementById('SoH').innerHTML = " " + score;

    //Stealth
    score = 0;
    if (document.getElementById('CkStealth').checked == true) {
        score += +ProfBonus;
    }
    score += +dexMod;
    document.getElementById('Stealth').innerHTML = " " + score;

    //Survival
    score = 0;
    if (document.getElementById('CkSurvival').checked == true) {
        score += +ProfBonus;
    }
    score += +wisMod;
    document.getElementById('Survival').innerHTML = " " + score;
}

function CalcAll() {
    CalcScores();

    var SavingThrowArray = [];
    var SkillArray = [];

    var current;
    for (var x = 0; x < 18; x++) {
            switch (x) {
                case 0:
                    current = document.getElementById('CkAcrobatics').checked;
                    break;
                case 1:
                    current = document.getElementById('CkAnimHand').checked;
                    break;
                case 2:
                    current = document.getElementById('CkArcana').checked;
                    break;
                case 3:
                    current = document.getElementById('CkAthletics').checked;
                    break;
                case 4:
                    current = document.getElementById('CkDeception').checked;
                    break;
                case 5:
                    current = document.getElementById('CkHistory').checked;
                    break;
                case 6:
                    current = document.getElementById('CkInsight').checked;
                    break;
                case 7:
                    current = document.getElementById('CkIntimidation').checked;
                    break;
                case 8:
                    current = document.getElementById('CkInvestigation').checked;
                    break;
                case 9:
                    current = document.getElementById('CkMedicine').checked;
                    break;
                case 10:
                    current = document.getElementById('CkNature').checked;
                    break;
                case 11:
                    current = document.getElementById('CkPerception').checked;
                    break;
                case 12:
                    current = document.getElementById('CkPerformance').checked;
                    break;
                case 13:
                    current = document.getElementById('CkPersuasion').checked;
                    break;
                case 14:
                    current = document.getElementById('CkReligion').checked;
                    break;
                case 15:
                    current = document.getElementById('CkSoH').checked;
                    break;
                case 16:
                    current = document.getElementById('CkStealth').checked;
                    break;
                case 17:
                    current = document.getElementById('CkSurvival').checked;
                    break;
            }
            if (current == true) {
                SkillArray[x] = "1";
            }
            else {
                SkillArray[x] = "0";
            }
    }
    var resultSkill = "";
    for(var x = 0; x<18; x++){
        result += SkillArray[x];
    }
    var test = parseInt(result, 2);
    alert(test);
}