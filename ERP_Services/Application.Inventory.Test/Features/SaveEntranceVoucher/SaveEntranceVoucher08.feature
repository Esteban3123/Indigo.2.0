#language: es
Característica: SaveEntranceVoucher08
	Para ...
	Como un usuario
	Quiero ...

#Escenario: Guardar Comprobante de Entrada
#	Dado Consulto el comprobante de entrada 7
#	Y El comprobante de entrada existe
#	Cuando Yo guardo el comprobante de entrada
#	Entonces Este es almacenado
	
Esquema del escenario: Guardar Comprobante de Entrada
	Dado Consulto el comprobante de entrada <comprobante_id>
	Y El comprobante de entrada existe
	Cuando Yo guardo el comprobante de entrada
	Entonces Este es almacenado

	Ejemplos: 
	| comprobante_id |
	| 83 |
    | 84 |
	| 85 |
	| 86 |
	| 87 |