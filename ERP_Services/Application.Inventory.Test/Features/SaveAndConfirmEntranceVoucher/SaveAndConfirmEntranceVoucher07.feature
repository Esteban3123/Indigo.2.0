#language: es
Característica: SaveAndConfirmEntranceVoucher07
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar y Confirmar Comprobante de Entrada
#	Dado Genero el comprobande de  entrada
#	Y Yo guardo el comprobante de entrada
#	Cuando Yo confirmo el comprobante de entrada
#	Entonces Este es almacenado y Confirmado
	
Esquema del escenario: Guardar y Confirmar Comprobante de Entrada
	Dado Genero el comprobante de  entrada <REG_No>
	Y Yo guardo y confirmo el comprobante de entrada <comprobante_id> <REG_No>
	Entonces Este es almacenado y Confirmado

	Ejemplos: 
	| comprobante_id | REG_No |
	| 22             |   7    |
