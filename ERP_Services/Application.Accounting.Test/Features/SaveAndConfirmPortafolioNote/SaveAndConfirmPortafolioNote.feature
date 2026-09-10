#language: es
Característica: SavePaymentNotesComplete
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Notas debito y credito CxC
#	Dado Genero los maestros y detalles
#	Y Yo guardo y confirmo las Notas debito y credito
#	Entonces Estos son almacenados y Confirmados
	
Esquema del escenario: Guardar y Confirmar Notas debito y credito CxC
	Dado Genero los maestros y detalles
	Y Guardo y confirmo las Notas debito y credito <Registro_No>
	Entonces Estas son almacenados y Confirmados

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
