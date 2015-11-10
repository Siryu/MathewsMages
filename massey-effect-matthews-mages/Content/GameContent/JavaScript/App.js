//   *                                                                 
//   (  `                              (    (               )   (  (     
//   )\))(     )         (  (      (   )\ ) )\ )   (     ( /(   )\))(    
//   ((_)()\ ( /( (  (   ))\ )\ )   )\ (()/((()/(  ))\ (  )\()) ((_)()\   
//   (_()((_))(_)))\ )\ /((_|()/(  ((_) /(_))/(_))/((_))\(_))/   (()((_)  
//   |  \/  ((_)_((_|(_|_))  )(_)) | __(_) _(_) _(_)) ((_) |_     | __|   
//   | |\/| / _` (_-<_-< -_)| || | | _| |  _||  _/ -_) _||  _|    |__ \   
//   |_|  |_\__,_/__/__|___| \_, | |___||_|  |_| \___\__| \__|    |___/   
//      *                    |__/                 *                       
//    (  `           )   )   )           (      (  `                      
//    )\))(     ) ( /(( /(( /(   (  (  ( )\     )\))(     ) (  (    (     
//   ((_)()\ ( /( )\())\())\()) ))\ )\))((_|   ((_)()\ ( /( )\))(  ))\(   
//   (_()((_))(_)|_))(_))((_)\ /((_|(_)()\ )\  (_()((_))(_)|(_))\ /((_)\  
//   |  \/  ((_)_| |_| |_| |(_|_)) _(()((_|(_) |  \/  ((_)_ (()(_|_))((_) 
//   | |\/| / _` |  _|  _| ' \/ -_)\ V  V (_-< | |\/| / _` / _` |/ -_|_-< 
//   |_|  |_\__,_|\__|\__|_||_\___| \_/\_//__/ |_|  |_\__,_\__, |\___/__/ 
//                                                         |___/          
//
//   Created By: Corey Massey, Kurt Peterson, Wayne Maree, Matthew Staples
//
//////////////////////////////////////////////////////////////////////////

//////////////////////
// Global Variables //
//////////////////////

//Canvas
var canvas = document.getElementById("canvas");
canvas.width = 800;
canvas.height = 800;
var ctx = canvas.getContext("2d");
var mapHeight;
var mapWidth;

//Game Loop
var lastTime;
var lastFire = Date.now();
var lastDamage = Date.now();
var drawDamageRect = false;
var gameOver = false;

//Game Objects
var deathScreen = new Image();
var lastKey = '';
var lastDirection = '';
var monstersKilled = 0;
var roomsTraveled = 0;
var currentHealth = 100;
var maxhealth = 100;
var terrainPattern;
var monsters = [];
var dropItems = [];
var weapons = [];
var weaponsAnimations = [];
var jsonTerrain = [];
var renderQueue = [];
var bloods = [];
var mapObj = {
    pos: [0, 0]
}
var guy = {
    maxHealth: 100,
    currentHealth: 100,
    pos: [canvas.width / 2, canvas.height / 2],
    size: [140, 185],
    sprite: new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 2], [160, 135], 24, [0], null, false, true)
}
var weapon = {
    pos: [450, 450],
    sprite: new Sprite('../Content/GameContent/Images/fireball.png', [0, 0], [58, 61], 10, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12], null, false, true)
}
var levelExit = {
    pos: [1500, 900],
    sprite: new Sprite('../Content/GameContent/Images/teleport_512.png', [0, 256], [512, 256], 8, [0, 1, 2, 3], 'vertical', false, true)
    //image: '../Content/GameContent/Images/Red_Portal.png'
}

//Game Constants (Speed in pixels per second)
var playerSpeed = 600;
var playerWidth = 140;
var playerHeight = 185;
var playerCollisionWidth = 50;
var playerCollisionHeight = 50;
var playerAttackSpeed = 300;
var enemySpeed = 100;
var invulnTime = 1500;
var damageRectDisplayTime = 200;

////////////////////////
// Asynchronous Calls //
////////////////////////

//loading Room with Monsters and Terrain
function loadRoom() {
    $.getJSON("/Game/GenerateRoom", function (json) {
        jsonRoom = json;

        totalMonstersKilled = jsonRoom.MonstersKilled;
        totalRoomsTraveled = jsonRoom.RoomsTraveled;

        terrainPattern = jsonRoom.Image;
        mapWidth = jsonRoom.Width;
        mapHeight = jsonRoom.Height;

        for (var m in jsonRoom.monsters) {
            jsonRoom.monsters[m].pos = [jsonRoom.monsters[m].StartPosition.X + mapObj.pos[0], jsonRoom.monsters[m].StartPosition.Y + mapObj.pos[1]];
            jsonRoom.monsters[m].width = jsonRoom.monsters[m].Width;
            jsonRoom.monsters[m].height = jsonRoom.monsters[m].Height;
            jsonRoom.monsters[m].currentHealth = jsonRoom.monsters[m].MaxHealth;
            jsonRoom.monsters[m].sprite = new Sprite(jsonRoom.monsters[m].Image, [0, 0], [155, 192], jsonRoom.monsters[m].AnimationSpeed, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], null, false, true);
            monsters.push(jsonRoom.monsters[m]);
        }

        for (var t in jsonRoom.TerrainObjects) {
            jsonRoom.TerrainObjects[t].posX = jsonRoom.TerrainObjects[t].X + mapObj.pos[0];
            jsonRoom.TerrainObjects[t].posY = jsonRoom.TerrainObjects[t].Y + mapObj.pos[1];
            jsonRoom.TerrainObjects[t].pos = [jsonRoom.TerrainObjects[t].X + mapObj.pos[0], jsonRoom.TerrainObjects[t].Y + mapObj.pos[1]];
            jsonRoom.TerrainObjects[t].sprite = new Sprite(jsonRoom.TerrainObjects[t].Image, [0, 0], [jsonRoom.TerrainObjects[t].Width, jsonRoom.TerrainObjects[t].Height], 1, [0], null, false, true);
            jsonTerrain.push(jsonRoom.TerrainObjects[t]);
        }

        levelExit.pos[0] = mapWidth - 500 + mapObj.pos[0];
        levelExit.pos[1] = mapHeight - 500 + mapObj.pos[1];
    });
};

// expects to recieve two ints first is monsters killed, then rooms traveled.
function updateKilledAndTraveled(jsonStats) {
    $.ajax({
        type: "POST",
        url: '/Game/UpdateRoomTraveledAndMonstersKilled',
        data: { list: jsonStats },
        cache: false,
        dataType: "json",
        //error: function (x, e, data) {
        //    alert(data);
        //}
    });
    roomsTraveled = 0;
    monstersKilled = 0;
}

// expects to recieve two ints first is attack, then defense.
function updateItemPickedUp(jsonStats) {
    $.ajax({
        type: "POST",
        url: '/Game/UpdateUser',
        data: { list: jsonStats },
        cache: false,
        dataType: "json",
    });
}

function resetPlayerStats(resetStats) {
    $.ajax({
        type: "POST",
        url: '/Game/ResetPlayerStats',
        data: { list: resetStats },
        cache: false
    });
}

function loadUser() {
    $.getJSON("/Game/GetUser", function (json) {
        guy.level = json.Level;
        guy.attack = json.Attack;
        guy.defense = json.Defense;
        guy.monstersKilled = json.MonstersKilled;
        guy.roomsTraveled = json.RoomsTraveled;
    });
}

function unLoadRoom() {
    monsters = [];
    jsonTerrain = [];
    renderQueue = [];
    levelExit.pos = [-2000, -2000];
}

//////////////////////
// Game Functions   //
//////////////////////

//Initial Setup (Loading of assets)
resources.load([
    '../Content/GameContent/Images/fireball.png',
    '../Content/GameContent/Images/blood_sprite6.png',
    '../Content/GameContent/Images/terrain.png',
    '../Content/GameContent/Images/bastion_sprite_sheet1.png',
    '../Content/GameContent/Images/attack_sprite.png',
    '../Content/GameContent/Images/Red_Portal.png',
    '../Content/GameContent/Images/teleporter.gif',
    '../Content/GameContent/Images/powerup.png',
    '../Content/GameContent/Images/powerup_defense.png',
    '../Content/GameContent/Images/powerup_boss.png',
    '../Content/GameContent/Images/teleport_512.png',
    '../Content/GameContent/Images/tree01.png',
    '../Content/GameContent/Images/rock01.png',
    '../Content/GameContent/Images/game-hud.png',
    '../Content/GameContent/Images/death_screen.png',
    '../Content/GameContent/Images/slime_sprite4.png',
    '../Content/GameContent/Images/slime_sprite_red.png',
    '../Content/GameContent/Images/slime_sprite_black.png',
    '../Content/GameContent/Images/slime_sprite_green.png',
    '../Content/GameContent/Images/red_tree.png',
    '../Content/GameContent/Images/green_tree.png',
    '../Content/GameContent/Images/wayne_rock.png'
]);
resources.onReady(init);

// A cross-browser requestAnimationFrame
var requestAnimFrame = (function () {
    return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function (callback) {
        window.setTimeout(callback, 1000 / 60);
    };
})();

function init() {
    loadUser();
    loadRoom();
    lastTime = Date.now();
    restartInvuln = false;
    mapObj.pos = [400, 400];
    main();
}

//The main game loop
function main() {
    var now = Date.now();
    var dt = (now - lastTime) / 1000.0;
    update(dt);
    render();
    lastTime = now;
    requestAnimFrame(main);
};

// Update game objects
function update(dt) {
    handleInput(dt);
    updateEntities(dt);
    checkCollisions(dt);
    monsterMovedUpdate(dt);
    //Update score here
};

var lastBossJump = Date.now();
var bossJumpDelay = 1500;
function monsterMovedUpdate(dt) {

    for (var i = 0; i < monsters.length; i++) {
        if (monsters[i].pos[0] < canvas.width && monsters[i].pos[0] > (canvas.width / 2) + (playerWidth / 2) - 25 && monsters[i].pos[1] < canvas.height && monsters[i].pos[1] > 0) {
            monsters[i].pos[0] -= monsters[i].MoveSpeed * dt;
            if (monsters[i].Name == "Boss") {
                if (Date.now() - lastBossJump > bossJumpDelay) {
                    // monsters[i].pos[0] -= (monsters[i].MoveSpeed * 3) * dt;
                    monsters[i].pos[0] = canvas.width / 2 + 150;
                    lastBossJump = Date.now();
                }
            }
        }
        if (monsters[i].pos[0] > (-monsters[i].width) && monsters[i].pos[0] < (canvas.width / 2) - (playerWidth / 2) + 5 && monsters[i].pos[1] < canvas.height && monsters[i].pos[1] > 0) {
            monsters[i].pos[0] += monsters[i].MoveSpeed * dt;
            if (monsters[i].Name == "Boss") {
                if (Date.now() - lastBossJump > bossJumpDelay) {
                    //monsters[i].pos[0] += (monsters[i].MoveSpeed * 3) * dt;
                    monsters[i].pos[0] = canvas.width / 2 - 150;
                    lastBossJump = Date.now();
                }
            }
        }
        if (monsters[i].pos[1] < canvas.height && monsters[i].pos[1] > (canvas.height / 2) + (playerWidth / 2) - 20 && monsters[i].pos[0] < canvas.width && monsters[i].pos[0] > 0) {
            monsters[i].pos[1] -= monsters[i].MoveSpeed * dt;
            if (monsters[i].Name == "Boss") {
                if (Date.now() - lastBossJump > bossJumpDelay) {
                    //monsters[i].pos[1] -= (monsters[i].MoveSpeed * 3) * dt;
                    monsters[i].pos[1] = canvas.height / 2 + 150;
                    lastBossJump = Date.now();
                }
            }
        }
        if (monsters[i].pos[1] > (-monsters[i].height) && monsters[i].pos[1] < (canvas.height / 2) - (playerHeight / 2) + 30 && monsters[i].pos[0] < canvas.width && monsters[i].pos[0] > 0) {
            monsters[i].pos[1] += monsters[i].MoveSpeed * dt;
            if (monsters[i].Name == "Boss") {
                if (Date.now() - lastBossJump > bossJumpDelay) {
                    //monsters[i].pos[1] += (monsters[i].MoveSpeed * 3) * dt;
                    monsters[i].pos[1] = canvas.height / 2 - 200;
                    lastBossJump = Date.now();
                }
            }
        }
    }
}

//Character Movement
function handleInput(dt) {
    if (input.isDown('UP') || input.isDown('w')) {
        if (lastKey != 'UP') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 142], [160, 135], 24, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40], null, false, false);
        }
        if (mapObj.pos[1] < canvas.height / 2) {
            //for (var i = 0; i < monsters.length; i++) {
            //    monsters[i].pos[1] += playerSpeed * dt;
            //}
            //for (var i = 0; i < jsonTerrain.length; i++) {
            //    jsonTerrain[i].pos[1] += playerSpeed * dt;
            //}
            mapObj.pos[1] += playerSpeed * dt;
            levelExit.pos[1] += playerSpeed * dt;
        }
        lastKey = 'UP';
        lastDirection = 'UP';
    }
    else if (input.isDown('DOWN') || input.isDown('s')) {
        if (lastKey != 'DOWN') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 426], [160, 135], 24, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40], null, false, false);
        }
        if (mapObj.pos[1] > -mapHeight + playerHeight + (canvas.height / 2)) {
            //for (var i = 0; i < monsters.length; i++) {
            //    monsters[i].pos[1] -= playerSpeed * dt;
            //}
            //for (var i = 0; i < jsonTerrain.length; i++) {
            //    jsonTerrain[i].pos[1] -= playerSpeed * dt;
            //}
            mapObj.pos[1] -= playerSpeed * dt;
            levelExit.pos[1] -= playerSpeed * dt;
        }
        lastKey = 'DOWN';
        lastDirection = 'DOWN';
    }
    else if (input.isDown('RIGHT') || input.isDown('a')) {
        if (lastKey != 'RIGHT') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 2], [160, 135], 24, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40], null, false, false);
        }
        if (mapObj.pos[0] > -mapWidth + playerWidth + (canvas.width / 2)) {
            //for (var i = 0; i < monsters.length; i++) {
            //    monsters[i].pos[0] -= playerSpeed * dt;
            //}
            //for (var i = 0; i < jsonTerrain.length; i++) {
            //    jsonTerrain[i].pos[0] -= playerSpeed * dt;
            //}
            mapObj.pos[0] -= playerSpeed * dt;
            levelExit.pos[0] -= playerSpeed * dt;
        }
        lastKey = 'RIGHT';
        lastDirection = 'RIGHT';
    }
    else if (input.isDown('LEFT') || input.isDown('d')) {
        if (lastKey != 'LEFT') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 284], [159, 135], 24, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40], null, false, false);
        }
        if (mapObj.pos[0] < canvas.width / 2) {

            //for (var i = 0; i < monsters.length; i++) {
            //    monsters[i].pos[0] += playerSpeed * dt;
            //}
            //for (var i = 0; i < jsonTerrain.length; i++) {
            //    jsonTerrain[i].pos[0] += playerSpeed * dt;
            //}
            mapObj.pos[0] += playerSpeed * dt;
            levelExit.pos[0] += playerSpeed * dt;
        }
        lastKey = 'LEFT';
        lastDirection = 'LEFT';
    }
    else {
        if (lastKey == 'RIGHT') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 2], [160, 135], 24, [0], null, false, true);
        } else if (lastKey == 'LEFT') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 284], [160, 135], 24, [0], null, false, true);
        } else if (lastKey == 'UP') {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 142], [160, 135], 24, [0], null, false, true);
        } else if (lastKey == "DOWN") {
            guy.sprite = new Sprite('../Content/GameContent/Images/bastion_sprite_sheet1.png', [10, 426], [160, 135], 24, [0], null, false, true);
        }
        lastKey = 'empty';
    }
    if (input.isDown('SPACE') && Date.now() - lastFire > playerAttackSpeed) {
        if (lastDirection == 'RIGHT') {
            weapon.pos[0] = guy.pos[0] + 175;
            weapon.pos[1] = guy.pos[1] + 50;
        } else if (lastDirection == 'LEFT') {
            weapon.pos[0] = guy.pos[0] - guy.size[0] / 2 - 25;
            weapon.pos[1] = guy.pos[1] + 50;
        } else if (lastDirection == 'UP') {
            weapon.pos[0] = guy.pos[0] + 40;
            weapon.pos[1] = guy.pos[1] - 75;
        } else if (lastDirection == 'DOWN') {
            weapon.pos[0] = guy.pos[0] + 50;
            weapon.pos[1] = guy.pos[1] + 175;
        }

        weapon.sprite = new Sprite('../Content/GameContent/Images/fireball.png', [0, 0], [58, 61], 10, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12], null, true, true);
        weapons.push(weapon);
        weaponsAnimations.push(weapon);

        lastFire = Date.now();
    }
}

function updateEntities(dt) {

    levelExit.sprite.update(dt);
    guy.sprite.update(dt);

    if (Date.now() - lastDamage > damageRectDisplayTime) {
        drawDamageRect = false;
    }

    //TODO: Update monsters positions
    updateLocations(dt, monsters, false);
    //updateLocations(dt, levelExit, false);
    updateLocations(dt, bloods, true);
    updateLocations(dt, dropItems, false);
    updateLocations(dt, renderQueue, false);
    updateLocations(dt, jsonTerrain, false);

    //update terrain
    //for (var i = 0; i < jsonTerrain.length; i++) {
    // jsonTerrain[i].sprite.update(dt);
    //var t = new Image();
    //t.src = jsonTerrain[i].Image;
    //ctx.drawImage(t, jsonTerrain[i].pos[0], jsonTerrain[i].pos[1]);
    //}

    //update monster animations
    for (var i = 0; i < monsters.length; i++) {
        monsters[i].sprite.update(dt);
    }

    //update the weapons animations
    for (var i = 0; i < weaponsAnimations.length; i++) {
        weaponsAnimations[i].sprite.update(dt);
        if (weaponsAnimations[i].sprite.done) {
            weaponsAnimations.splice(i, 1);
            i--;
        }
    }

    //update the weapons
    for (var i = 0; i < weapons.length; i++) {
        weapons[i].sprite.update(dt);
        if (weapons[i].sprite.done) {
            weapons.splice(i, 1);
            i--;
        }
    }
}

function updateLocations(dt, entityList, isAnimated) {
    //Checks to make sure the player isn't running into a border. If they aren't, updates the entity's position
    for (var i = 0; i < entityList.length; i++) {
        if (lastKey == 'DOWN') {
            if (mapObj.pos[1] > -mapHeight + playerHeight + (canvas.height / 2)) {
                entityList[i].pos[1] -= playerSpeed * dt;
            }
        } else if (lastKey == 'UP') {
            if (mapObj.pos[1] < canvas.height / 2) {
                entityList[i].pos[1] += playerSpeed * dt;
            }
        } else if (lastKey == 'RIGHT') {
            if (mapObj.pos[0] > -mapWidth + playerWidth + (canvas.width / 2)) {
                entityList[i].pos[0] -= playerSpeed * dt;
            }
        } else if (lastKey == 'LEFT') {
            if (mapObj.pos[0] < canvas.width / 2) {
                entityList[i].pos[0] += playerSpeed * dt;
            }
        }

        if (isAnimated) {
            entityList[i].sprite.update(dt);
            if (entityList[i].sprite.done) {
                entityList.splice(i, 1);
                i--;
            }
        }
    }
}

// Collisions
function collides(x, y, r, b, x2, y2, r2, b2) {
    return !(r <= x2 || x > r2 || b <= y2 || y > b2);
}

function boxCollides(pos, size, pos2, size2) {
    return collides(pos[0], pos[1], pos[0] + size[0], pos[1] + size[1], pos2[0], pos2[1], pos2[0] + size2[0], pos2[1] + size2[1]);
}

function checkCollisions(dt) {

    var entityPos;
    var entitySize;
    var characterPos;
    var characterSize;
    var weaponPos;
    var weaponSize;

    //Check collision (bounding boxes) of terrain objects
    for (var i = 0; i < jsonTerrain.length; i++) {
        entityPos = [jsonTerrain[i].pos[0], jsonTerrain[i].pos[1]];
        entitySize = [jsonTerrain[i].Width, jsonTerrain[i].Height];

        characterPos = [canvas.width / 2, canvas.height / 2 + 90]; //the +90 moves the collision box closer to the player's feet
        characterSize = [playerCollisionWidth, playerCollisionHeight];



        if (boxCollides(entityPos, entitySize, characterPos, characterSize)) {
            console.log("adding item to render queue");
            //console.log("object position: " + (jsonTerrain[i].Y - jsonTerrain[i].CollisionY));
            // console.log("left: " + (jsonTerrain[i].posX + jsonTerrain[i].CollisionX) + " right: " + (jsonTerrain[i].posX + jsonTerrain[i].CollisionX + jsonTerrain[i].CollisionWidth));
            //if (jsonTerrain[i].Y - jsonTerrain[i].CollisionY > 450 && (jsonTerrain[i].posX + jsonTerrain[i].CollisionX) < (canvas.width / 2 + playerCollisionWidth) && (jsonTerrain[i].posX + jsonTerrain[i].CollisionX + jsonTerrain[i].CollisionWidth) > (canvas.width / 2 + playerCollisionWidth)) {
            checkRealCollisionBox(dt, jsonTerrain[i]);

            if (jsonTerrain[i].pos[1] + jsonTerrain[i].Height - jsonTerrain[i].CollisionHeight > 450) {
                renderQueue.push(jsonTerrain[i]);
                jsonTerrain.splice(i, 1);
                i--;
            }
            //repositionPlayer(dt);
        }
    }

    //check collision for items in the render queue
    for (var i = 0; i < renderQueue.length; i++) {
        entityPos = [renderQueue[i].pos[0], renderQueue[i].pos[1]];
        entitySize = [renderQueue[i].Width, renderQueue[i].Height];

        characterPos = [canvas.width / 2, canvas.height / 2 + 90]; //the +90 moves the collision box closer to the player's feet
        characterSize = [playerCollisionWidth, playerCollisionHeight];



        if (boxCollides(entityPos, entitySize, characterPos, characterSize)) {
            //console.log("object position: " + (jsonTerrain[i].Y - jsonTerrain[i].CollisionY));
            // console.log("left: " + (jsonTerrain[i].posX + jsonTerrain[i].CollisionX) + " right: " + (jsonTerrain[i].posX + jsonTerrain[i].CollisionX + jsonTerrain[i].CollisionWidth));
            //if (jsonTerrain[i].Y - jsonTerrain[i].CollisionY > 450 && (jsonTerrain[i].posX + jsonTerrain[i].CollisionX) < (canvas.width / 2 + playerCollisionWidth) && (jsonTerrain[i].posX + jsonTerrain[i].CollisionX + jsonTerrain[i].CollisionWidth) > (canvas.width / 2 + playerCollisionWidth)) {
            checkRealCollisionBox(dt, renderQueue[i]);

            if (renderQueue[i].pos[1] + renderQueue[i].Height - renderQueue[i].CollisionHeight < 450) {
                jsonTerrain.push(renderQueue[i]);
                renderQueue.splice(i, 1);
                i--;
            }
            //repositionPlayer(dt);
        }
    }

    //Check collision for monsters
    for (var i = 0; i < monsters.length; i++) {
        entityPos = [monsters[i].pos[0], monsters[i].pos[1] + 90];
        entitySize = [monsters[i].width, monsters[i].height - 90];

        characterPos = [canvas.width / 2, canvas.height / 2 + 90]; //the +90 moves the collision box closer to the player's feet
        characterSize = [playerCollisionWidth, playerCollisionHeight];

        //if player is in the bounding box of the monster, check for collision
        if (boxCollides(entityPos, entitySize, characterPos, characterSize)) {
            if (Date.now() - lastDamage > invulnTime && !restartInvuln) {
                var damage = monsters[i].Attack - guy.defense;
                console.log(damage);
                if (damage <= 0) {
                    damage = 1;
                }
                guy.currentHealth -= damage;
                if (guy.currentHealth <= 0) {
                    guy.currentHealth = 0;
                    endGame();
                }
                lastDamage = Date.now();
                drawDamageRect = true;
            }


            //checkRealCollisionBox(dt, monsters[i]);
        }
    }

    for (var i = 0; i < dropItems.length; i++) {
        //console.log(i + " items in drop")
        entityPos = [dropItems[i].pos[0], dropItems[i].pos[1]];
        entitySize = [dropItems[i].sprite.size[0], dropItems[i].sprite.size[1]];

        characterPos = [canvas.width / 2, canvas.height / 2 + 90]; //the +90 moves the collision box closer to the player's feet
        characterSize = [playerCollisionWidth, playerCollisionHeight];

        //if player is in the bounding box of the item, pick it up and update his stats
        if (boxCollides(entityPos, entitySize, characterPos, characterSize)) {
            guy.attack += dropItems[i].attack;
            guy.defense += dropItems[i].defense;
            updateItemPickedUp([dropItems[i].attack, dropItems[i].defense]);
            dropItems.splice(i, 1);
            i--;

            //heal the player
            if (guy.currentHealth + 10 < guy.maxHealth) {
                guy.currentHealth += 10;
            } else {
                guy.currentHealth = guy.maxHealth;
            }
        }
    }

    //Check collisions between weapons and monsters
    for (var i = 0; i < monsters.length; i++) {
        entityPos = [monsters[i].pos[0], monsters[i].pos[1]];
        entitySize = [monsters[i].width, monsters[i].height];

        for (var j = 0; j < weapons.length; j++) {
            weaponPos = weapons[j].pos;
            weaponSize = weapons[j].sprite.size;
            if (boxCollides(entityPos, entitySize, weaponPos, weaponSize)) {
                monsters[i].currentHealth -= guy.attack;
                if (monsters[i].currentHealth <= 0) {

                    monstersKilled++;
                    guy.monstersKilled++;

                    resetPlayerStats(["false", guy.monstersKilled]);
                    // Make it rain blood
                    bloods.push({
                        pos: entityPos,
                        sprite: new Sprite('../Content/GameContent/Images/blood_sprite6.png', [0, 0], [249, 240], 24, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9], null, true, true)
                    });
                    //If the monster has an item, add it to the array of Items
                    if (monsters[i].DropItem != null) {
                        dropItems.push({
                            //image: powerUp,
                            sprite: new Sprite(monsters[i].DropItem.Image, [0, 0], [105, 152], 1, [0], null, false, true),
                            attack: monsters[i].DropItem.Attack,
                            defense: monsters[i].DropItem.Defense,
                            pos: [monsters[i].pos[0], monsters[i].pos[1]]
                        });
                    }
                    //heal the player if the monster was a boss
                    if (monsters[i].Name == "Boss") {
                        if (guy.currentHealth + 50 < guy.maxHealth) {
                            guy.currentHealth += 50;
                        } else {
                            guy.currentHealth = guy.maxHealth;
                        }
                    }

                    monsters.splice(i, 1);
                    i--;
                }
                //Weapon can only damage one monster, so remove it after it collides
                weapons.splice(j, 1);
                j--;
                break;
            }
        }
    }
    // levelExit detection w/ portal
    var pos = [];
    pos.push(levelExit.pos[0] + 130);
    pos.push(levelExit.pos[1]);
    var size = [];
    size.push(120);
    size.push(60);

    var size2 = [5, 5];
    var pos2 = [];
    pos2.push(canvas.width / 2);
    pos2.push(canvas.height / 2);

    if (boxCollides(pos, size, pos2, size2)) {
        roomsTraveled++;
        guy.roomsTraveled++;
        $("canvas").fadeOut(1000, "swing", function () {
            mapObj.pos = [400, 400];
            loadRoom();
            var jsonStats = [monstersKilled, roomsTraveled];
            updateKilledAndTraveled(jsonStats);
            $("canvas").fadeIn(1000, "swing");
        });
        unLoadRoom();
    }
}

function checkRealCollisionBox(dt, entity) {
    //console.log("checking real collision: X: " + (entity.posX + entity.CollisionX) + " Y: " + (entity.posY + entity.CollisionY) + " H: " + entity.CollisionHeight + " W: " + entity.CollisionWidth );
    var entityPos = [entity.pos[0] + entity.CollisionX - (playerCollisionWidth / 1.5 + 15), entity.pos[1] + entity.CollisionY - (playerCollisionHeight * 1.5)];
    var entitySize = [entity.CollisionWidth, entity.CollisionHeight];

    var characterSize = [playerCollisionWidth, playerCollisionHeight];
    var characterPos = [canvas.width / 2, canvas.height / 2];
    //console.log("Character X: " + canvas.width / 2 + " Y: " + canvas.height / 2);

    //if player is in the bounding box of the monster, check for collision
    //TODO: make this deal damage to the player
    if (boxCollides(entityPos, entitySize, characterPos, characterSize)) {
        //repositionPlayer(dt);
        repositionAssets(dt);
    }
}
function repositionAssets(dt) {
    var assets = [monsters, jsonTerrain, bloods, dropItems, renderQueue]

    if (lastKey == 'DOWN') {
        mapObj.pos[1] += playerSpeed * dt;
        levelExit.pos[1] += playerSpeed * dt;
    } else if (lastKey == 'UP') {
        mapObj.pos[1] -= playerSpeed * dt;
        levelExit.pos[1] -= playerSpeed * dt;
    } else if (lastKey == 'RIGHT') {
        mapObj.pos[0] += playerSpeed * dt;
        levelExit.pos[0] += playerSpeed * dt;
    } else if (lastKey == 'LEFT') {
        mapObj.pos[0] -= playerSpeed * dt;
        levelExit.pos[0] -= playerSpeed * dt;
    }
    assets.forEach(function (asset) {
        if (lastKey == 'DOWN') {
            for (var i = 0; i < asset.length; i++) {
                asset[i].pos[1] += playerSpeed * dt;
            }
        } else if (lastKey == 'UP') {
            for (var i = 0; i < asset.length; i++) {
                asset[i].pos[1] -= playerSpeed * dt;
            }
        } else if (lastKey == 'RIGHT') {
            for (var i = 0; i < asset.length; i++) {
                asset[i].pos[0] += playerSpeed * dt;
            }
        } else if (lastKey == 'LEFT') {
            for (var i = 0; i < asset.length; i++) {
                asset[i].pos[0] -= playerSpeed * dt;
            }
        }

    })
}
//function repositionPlayer(dt) {
//    if (lastKey == 'DOWN') {
//        mapObj.pos[1] += playerSpeed * dt;
//        levelExit.pos[1] += playerSpeed * dt;

//        for (var i = 0; i < monsters.length; i++) {
//            monsters[i].pos[1] += playerSpeed * dt;
//        }
//        for (var i = 0; i < jsonTerrain.length; i++) {
//            jsonTerrain[i].pos[1] += playerSpeed * dt;
//        }
//    } else if (lastKey == 'UP') {
//        mapObj.pos[1] -= playerSpeed * dt;
//        levelExit.pos[1] -= playerSpeed * dt;

//        for (var i = 0; i < monsters.length; i++) {
//            monsters[i].pos[1] -= playerSpeed * dt;
//        }
//        for (var i = 0; i < jsonTerrain.length; i++) {
//            jsonTerrain[i].pos[1] -= playerSpeed * dt;
//        }
//    } else if (lastKey == 'RIGHT') {
//        mapObj.pos[0] += playerSpeed * dt;
//        levelExit.pos[0] += playerSpeed * dt;

//        for (var i = 0; i < monsters.length; i++) {
//            monsters[i].pos[0] += playerSpeed * dt;
//        }
//        for (var i = 0; i < jsonTerrain.length; i++) {
//            jsonTerrain[i].pos[0] += playerSpeed * dt;
//        }
//    } else if (lastKey == 'LEFT') {
//        mapObj.pos[0] -= playerSpeed * dt;
//        levelExit.pos[0] -= playerSpeed * dt;

//        for (var i = 0; i < monsters.length; i++) {
//            monsters[i].pos[0] -= playerSpeed * dt;
//        }
//        for (var i = 0; i < jsonTerrain.length; i++) {
//            jsonTerrain[i].pos[0] -= playerSpeed * dt;
//        }
//    }
//}

// Draw everything
function render() {
    //Black background
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    var map = new Image();
    map.src = terrainPattern;

    //Draw the map
    ctx.drawImage(map, mapObj.pos[0], mapObj.pos[1]);

    //Draw dropItems
    renderEntities(dropItems);
    //Draw the monsters' health bars
    for (var i = 0; i < monsters.length; i++) {
        drawHealthBar(monsters[i]);

    }
    //Draw the monsters
    //TODO: Add these to updateEntities and draw them from there
    //for (var i = 0; i < monsters.length; i++) {

    //    var m = new Image();
    //    m.src = monsters[i].Image;
    //    ctx.drawImage(m, monsters[i].pos[0], monsters[i].pos[1]);

    //}
    renderEntities(monsters);

    //Draw the terrain objects
    //TODO: Add these to updateEntities and draw them from there
    renderEntities(jsonTerrain);

    //var levelExitImage = new Image();
    //levelExitImage.src = levelExit.image;
    //ctx.drawImage(levelExitImage, levelExit.pos[0], levelExit.pos[1]);

    renderEntity(levelExit);
    renderEntity(guy);
    renderEntities(renderQueue);
    //PlayerHealthDisplay();
    //renderEntities(weapons);
    renderEntities(weaponsAnimations);
    renderEntities(bloods);

    if (drawDamageRect) {
        ctx.fillStyle = 'rgba(225,0,0,0.5)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);
    }

    if (gameOver) {
        deathScreen.src = '../Content/GameContent/Images/death_screen.png'
        ctx.fillStyle = 'rgba(0,0,0,1)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.drawImage(deathScreen, 0, 0);

        ctx.textAlign = 'Center';
        ctx.fillStyle = "#DEDEDE";
        ctx.font = "300 66px Open Sans";
        ctx.fillText("You Have Died", canvas.width / 2, 325);
    }
    renderHud();
};

function renderEntities(list) {
    for (var i = 0; i < list.length; i++) {
        renderEntity(list[i]);
    }
}

function renderEntity(entity) {
    ctx.save();
    ctx.translate(entity.pos[0], entity.pos[1]);
    entity.sprite.render(ctx);
    ctx.restore();
}

function endGame() {
    gameOver = true;
    monsters = [];
    $("#restart").fadeIn(1500);
    resetPlayerStats(["true", 1]);
    //guy.attack = 1;
    // guy.defense = 1;

    //guy = [];
}

///////////////////////
// Monster Functions //
///////////////////////

function drawHealthBar(monster) {
    //healthbar border
    ctx.fillStyle = "#000000";
    ctx.fillRect(monster.pos[0] - 1, monster.pos[1] + monster.height + 44, monster.width + 2, 10);

    //damage
    ctx.fillStyle = "#b61c1c";
    ctx.fillRect(monster.pos[0], monster.pos[1] + monster.height + 45, monster.width, 8);

    //remaining life
    ctx.fillStyle = "#455cbf";
    ctx.fillRect(monster.pos[0], monster.pos[1] + monster.height + 45, getHealthBarSize(monster.width, monster.currentHealth, monster.MaxHealth), 8);
}
function getHealthBarSize(monsterWidth, currentHealth, maxHealth) {
    return monsterWidth * (currentHealth / maxHealth);
}


///////////////////////
// UI Functions      //
///////////////////////
var hud = new Image();
hud.src = '../Content/GameContent/Images/game-hud.png';
function renderHud() {
    ctx.drawImage(hud, 0, canvas.height - 93); //93 is the height of the hud

    ctx.textAlign = 'left';
    ctx.fillStyle = "#DEDEDE";
    ctx.font = "300 20px Open Sans";
    ctx.fillText("Monsters Killed: " + guy.monstersKilled, 10, 750, 160);
    ctx.fillText("Rooms Travelled: " + guy.roomsTraveled, 10, 780, 160);
    ctx.fillText("Attack: " + guy.attack, 645, 750, 160);
    ctx.fillText("Defense: " + guy.defense, 645, 780, 160);

    ctx.textAlign = 'center';
    //ctx.fillText("Current Health", 400, 785);
    var remain = monsters.length == 1 ? "Remains" : "Remain";
    var monsterString = monsters.length == 1 ? " Monster " : " Monsters ";
    ctx.fillText(monsters.length + monsterString + remain, 400, 785);


    PlayerHealthDisplay();
}
function PlayerHealthDisplay() {
    //healthbar border
    ctx.fillStyle = "#000000";
    //ctx.fillRect((canvas.width / 2) - 100 - 2, 700 - 2, 200 + 4, 20);
    ctx.fillRect(200 - 2, 740 - 2, 400 + 4, 24);

    //damage
    ctx.fillStyle = "#b61c1c";
    //ctx.fillRect((canvas.width / 2 - 100), 700, 200, 16);
    ctx.fillRect(200, 740, 400, 20);

    //remaining life
    ctx.fillStyle = "#455cbf";
    //ctx.fillRect((canvas.width / 2 - 100), 700, (200 * (currentHealth / maxhealth)), 16);
    ctx.fillRect(200, 740, getHealthBarSize(400, guy.currentHealth, guy.maxHealth), 20);
}

var restartInvuln = false;
///////////////////////
// Restarting        //
///////////////////////
var restart = document.getElementById("restart");
restart.onclick = function () {
    restartInvuln = true;
    gameOver = false;
    $("#restart").fadeOut(50);
    //unloadRoom();
    monsters = [];
    jsonRoom = [];
    jsonTerrain = [];
    renderQueue = [];
    guy.currentHealth = guy.maxHealth;
    init();

};