'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 06-11-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports CommonEntities = Domain.Entities

#End Region
''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IBranchOfficeMain
#Region "Properties"

    ''' <summary>
    ''' Establece el datasource de las compañias
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Company As DevExpress.Xpo.XPInstantFeedbackSource

#End Region
End Interface
