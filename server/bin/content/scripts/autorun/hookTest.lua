function onLogin(index)
	name = getPlayer(index).Name
	cPrint(name.." has logged in")
end

function onLogout(index)
	cPrint(getPlayer(index).Name.." has logged out")
end

--[[function onTick()
	cPrint("SPAMMMMMMMM", "Red")
end ]]--