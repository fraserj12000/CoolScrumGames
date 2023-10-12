var myGamePiece;
var myObstacles = [];
var myScore;
var canvasParent;



function startGame(canvasObj) {
    
    canvasParent = canvasObj;
    myGamePiece = new component(30, 30, "../../images/game_images/AsteroidGame/asteroid.jpg", 10, 120, "image");
    myGamePiece.gravity = 0;
    myScore = new component("30px", "Arial", "white", 360, 40);
    myFinalScore = new component("60px", "Arial", "white", 360, 40);
    myGameArea.start();
}

var myGameArea = {
    parent: canvasParent,
    canvas: document.createElement("canvas"),
    start: function () {
        this.canvas.width = canvasParent.clientWidth;
        this.canvas.height = canvasParent.clientHeight;
        this.context = this.canvas.getContext("2d");
        canvasParent.appendChild(this.canvas);
        this.frameNo = 0;
        this.interval = setInterval(updateGameArea, 20);
        window.addEventListener('keydown', function (e) {
            myGameArea.key = e.keyCode;
        })
        window.addEventListener('keyup', function (e) {
            myGameArea.key = false;
        })
    },
    clear: function () {
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    }
}

function component(width, height, color, x, y, type) {
    this.type = type;
    if (type == "image") {
        this.image = new Image();
        this.image.src = color;
    }
    this.score = 0;
    this.width = width;
    this.height = height;
    this.speedX = 0;
    this.speedY = 0;
    this.x = x;
    this.y = y;
    this.gravity = 0;
    this.gravitySpeed = 0;
    this.update = function () {
        ctx = myGameArea.context;
        if (type == "image") {
            ctx.drawImage(this.image,
                this.x,
                this.y,
                this.width, this.height);
        } else {
            ctx.font = this.width + " " + this.height;
            ctx.fillStyle = color;
            ctx.fillText(this.text, this.x, this.y);
        }
    }
    this.newPos = function () {
        this.gravitySpeed += this.gravity;
        this.x += this.speedX;
        this.y += this.speedY + this.gravitySpeed;
    }
    this.crashWith = function (otherobj) {
        var myleft = this.x;
        var myright = this.x + (this.width);
        var mytop = this.y;
        var mybottom = this.y + (this.height);
        var otherleft = otherobj.x;
        var otherright = otherobj.x + (otherobj.width);
        var othertop = otherobj.y;
        var otherbottom = otherobj.y + (otherobj.height);
        var crash = true;
        if ((mybottom < othertop) || (mytop > otherbottom) || (myright < otherleft) || (myleft > otherright)) {
            crash = false;
        }
        return crash;
    }
}

function updateGameArea(crash) {
    var x, height, minHeight, maxHeight;
    for (i = 0; i < myObstacles.length; i += 1) {
        if (myGamePiece.crashWith(myObstacles[i])) {
            return;
        }
    }
    myGameArea.clear();
    myGameArea.frameNo += 1;
    if (myGameArea.frameNo == 1 || everyinterval(150)) {
        x = myGameArea.canvas.width;
        minHeight = 10;
        maxHeight = 600;
        obstacleminwidth = myGameArea.canvas.width * .10;
        obstaclemaxwidth = myGameArea.canvas.width * .15;
        obstacleminheight = myGameArea.canvas.height * .10;
        obstaclemaxheight = myGameArea.canvas.height * .15;
        obstacleWidth = Math.floor(Math.random() * (obstaclemaxwidth + obstacleminwidth) + obstacleminwidth)
        obstacleHeight = Math.floor(Math.random() * (obstaclemaxheight + obstacleminheight) + obstacleminheight)
        height = Math.floor(Math.random() * (maxHeight + minHeight + 3) + minHeight);
        myObstacles.push(new component(obstacleWidth, obstacleHeight, "../../images/game_images/AsteroidGame/spaceship.png", x, height, "image")); 
    }
    for (i = 0; i < myObstacles.length; i += 1) {
        myObstacles[i].x += -1;
        myObstacles[i].update();
    }
    myScore.text = "SCORE: " + myObstacles.length;
    myScore.update();
    if (myGameArea.key && myGameArea.key == 37) { myGamePiece.speedX += -.5; }
    if (myGameArea.key && myGameArea.key == 39) { myGamePiece.speedX += .5; }
    if (myGameArea.key && myGameArea.key == 38) { myGamePiece.speedY += -.5; }
    if (myGameArea.key && myGameArea.key == 40) { myGamePiece.speedY += .5; }
    myGamePiece.newPos();
    myGamePiece.update();
}

function everyinterval(n) {
    if ((myGameArea.frameNo / n) % 1 == 0) { return true; }
    return false;
}