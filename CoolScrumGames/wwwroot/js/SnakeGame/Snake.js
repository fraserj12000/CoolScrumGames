var myGameArea = {
    parent: null,
    canvas: document.createElement("canvas"),
    start: function () {
        this.canvas.width = this.parent.clientWidth;
        this.canvas.height = this.parent.clientHeight;
        this.context = this.canvas.getContext("2d");
        this.parent.appendChild(this.canvas);
        this.interval = setInterval(updateGameArea, 100); // Update the game area every 100 milliseconds
        this.canvas.addEventListener('keydown', function (e) {
            e.preventDefault(); // Prevent the default arrow key behavior
        });
    },
    clear: function () {
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    },
};

var snake = {
    body: [{ x: 10, y: 10 }],
    direction: 'right',
    gridSize: 20,
};

var food = {
    x: 15,
    y: 15,
};

function updateGameArea() {
    myGameArea.clear();
    moveSnake();
    drawFood();
    drawSnake();
}

function moveSnake() {
    var head = Object.assign({}, snake.body[0]);

    switch (snake.direction) {
        case 'up':
            head.y -= 1;
            break;
        case 'down':
            head.y += 1;
            break;
        case 'left':
            head.x -= 1;
            break;
        case 'right':
            head.x += 1;
            break;
    }

    snake.body.unshift(head);

    if (head.x === food.x && head.y === food.y) {
        // Snake ate the food, generate new food
        food.x = Math.floor(Math.random() * myGameArea.canvas.width / snake.gridSize);
        food.y = Math.floor(Math.random() * myGameArea.canvas.height / snake.gridSize);
    } else {
        snake.body.pop();
    }
}

function drawSnake() {
    var ctx = myGameArea.context;
    ctx.fillStyle = 'green';
    snake.body.forEach(function (segment) {
        ctx.fillRect(segment.x * snake.gridSize, segment.y * snake.gridSize, snake.gridSize, snake.gridSize);
    });
}

function drawFood() {
    var ctx = myGameArea.context;
    ctx.fillStyle = 'red';
    ctx.fillRect(food.x * snake.gridSize, food.y * snake.gridSize, snake.gridSize, snake.gridSize);
}

function changeDirection(event) {
    switch (event.keyCode) {
        case 37: // Left arrow key
            if (snake.direction !== 'right') {
                snake.direction = 'left';
            }
            break;
        case 38: // Up arrow key
            if (snake.direction !== 'down') {
                snake.direction = 'up';
            }
            break;
        case 39: // Right arrow key
            if (snake.direction !== 'left') {
                snake.direction = 'right';
            }
            break;
        case 40: // Down arrow key
            if (snake.direction !== 'up') {
                snake.direction = 'down';
            }
            break;
    }
}

function startSnakeGame(canvasObj) {
    myGameArea.parent = canvasObj;
    myGameArea.start();
    canvasObj.tabIndex = 1; // Make the canvas focusable
    canvasObj.addEventListener('keydown', function (e) {
        e.preventDefault(); // Prevent the default arrow key behavior
        changeDirection(e);
    });
    canvasObj.focus(); // Focus on the canvas to capture key events
}

startSnakeGame(document.getElementById('gameCanvas'));
