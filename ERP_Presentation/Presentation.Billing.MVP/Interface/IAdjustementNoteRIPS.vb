'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Oscar stiven astudillo
' Created          : 2024-11-18
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Public Interface IAdjustementNoteRIPS


#Region "Fields"
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object
#End Region


#Region "XPO"
    ''' <summary>
    ''' Datasource de los tipos de documentos
    ''' </summary>
    ''' <returns></returns>
    Property ADTIPOIDENTIFICAXpo As XPInstantFeedbackSource

#End Region


End Interface
