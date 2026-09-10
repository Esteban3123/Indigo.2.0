#language: es
Característica: SaveAndConfirmCashReceipts
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Recibos de Caja
#	Dado Genero los Maestros y Detalles
#	Y Guardo y confirmo los Recibos de caja 
#	Entonces Estos son almacenados y Confirmados
	
Esquema del escenario: Guardar y Confirmar Recibos de Caja
	Dado Genero los maestros y detalles
	Y Guardo y confirmo los Recibos de caja <Registro_No>
	Entonces Estos son almacenados y Confirmados

	Ejemplos: 
	| Registro_No |
	|      1      |
	|      2      |
	|      3      |
	|      4      |
	|      5      |
	|      6      |
	|      7      |
	|      8      |
	|      9      |
	|      10     |
	|      11     |
	|      12     |
	|      13     |
	|      14     |
	|      15     |
	|      16     |
