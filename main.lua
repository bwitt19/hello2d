function love.load()
    dtotal = 0

    -- Screen resolution considerations
    love.window.setMode(0,0)
    screen = {}
    screen.width, screen.height = love.window.getMode()

    -- Load Schnitzel sprite
    radda_icon = love.graphics.newImage("sprites/radda.png")

    -- Load initial "player" object
    player = {}
    -- Sprite qualities
    player.sprite = radda_icon
    player.x = math.random(screen.width)
    player.y = math.random(screen.height)
    player.rotate = 0
    player.sx = 0.5
    -- Movement qualities
    player.dx = 1
    player.dy = 1
    player.minv = 1
    player.maxv = 15
    player.a = 1.01

end

function love.update(dt)
    dtotal = dtotal + 1

    -- Move Schnitzel like a DVD menu
    -- Check if hit a wall, and adjust dx and dy accordingly
    if (player.x + (player.sprite:getWidth() * player.sx) >= screen.width) or (player.x <= 0) then
        player.dx = player.dx * -1
    end
    if (player.y + (player.sprite:getHeight() * player.sx) >= screen.height) or (player.y <= 0) then
        player.dy = player.dy * -1
    end

    -- Update player values
    player.x = player.x + player.dx
    player.y = player.y + player.dy


    -- Speed up on keypress
    if love.keyboard.isDown("space") then
        if (math.abs(player.dx * player.a) < player.maxv) then
            player.dx = player.dx * player.a
            player.dy = player.dy * player.a
        else
            player.dx = (player.dx / math.abs(player.dx)) * player.maxv
            player.dy = (player.dy / math.abs(player.dy)) * player.maxv
        end
    -- Slow down if key not pressed
    else
        if (math.abs(player.dx / player.a) > player.minv) then
            player.dx = player.dx / player.a
            player.dy = player.dy / player.a
        else
            player.dx = (player.dx / math.abs(player.dx)) * player.minv
            player.dy = (player.dy / math.abs(player.dy)) * player.minv
        end
    end

end

function love.draw()
    -- Display debug stats
    love.graphics.print("Frame count: " .. dtotal)
    love.graphics.print("FPS: " .. love.timer.getFPS(), 0, 15)
    love.graphics.print("x: " .. player.x .. " to " .. (player.x + (player.sx * player.sprite:getWidth())) .. " : " .. screen.width, 0, 30)
    love.graphics.print("y: " .. player.y .. " to " .. (player.y + (player.sx * player.sprite:getHeight())) .. " : " .. screen.height, 0, 45)
    love.graphics.print("v: " .. player.dx, 0, 60)

    -- DVD Menu style Schnitzel test
    love.graphics.draw(player.sprite, player.x, player.y, player.rotate, 
        player.sx)
end

function love.keyreleased(key)
    if key == "escape" then
       love.event.quit()
    end
 end