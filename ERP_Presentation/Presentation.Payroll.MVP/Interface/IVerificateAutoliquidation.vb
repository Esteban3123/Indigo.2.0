'***********************************************************************
' Assembly         : Presentacion.Payrol.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 17-07-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

Public Interface IVerificateAutoliquidation

    Inherits IcrudBase

#Region "Propiedades"

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Sub AsyncLoader(ByVal State As Boolean)

#End Region

End Interface
