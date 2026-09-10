'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/04/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq

#End Region

Public Interface IChangeHealthProfessional
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene o establece el id de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthProfessionalId As String

    ''' <summary>
    ''' Establece el datasource del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthProfessionalXpo As XPInstantFeedbackSource

End Interface
