'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Jose Luis Rojas
' Created          : 13-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Presentation.Controls
Imports Domain.Base.Entities
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IPayrollLiquidation
    Inherits IcrudBase

#Region "Properties"

      ''' <summary>
    ''' Propiedad que contiene el datasource de los concept group
    ''' </summary>
    WriteOnly Property DatasourceGroup As List(Of Group)

#End Region

End Interface
