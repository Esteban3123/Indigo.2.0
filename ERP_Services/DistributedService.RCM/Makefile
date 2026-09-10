# Comando para levantar contenedores
up:
	docker compose up -d  

# Comando para apagar contenedores
down:
	docker compose down

# Comando para acceder a la terminal del contenedor sqlserver
term_sql:
	docker exec -it sqlserver powershell.exe

# Comando para acceder a la terminal del contenedor de RCM
term_api:
	docker exec -it erp-rcm powershell.exe

# Comando para mostrar todos los contenedores
conts: 
	docker ps -a 

# Comando para mostrar todas las imágenes
imgs: 
	docker images