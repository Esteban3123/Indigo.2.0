'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/06/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetReclassificationRepository
    Inherits IRepository(Of FixedAssetReclassification)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetReclassificationById(Id As Integer, Optional tracking As Boolean = True) As FixedAssetReclassification

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetReclassificationByCode(Code As String) As FixedAssetReclassification

    ''' <summary>
    ''' Sp para guardar y confirmar la reclasificación
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_GenerateJournalVoucherByFixedAssetReclassification(Xml As String, CodeUser As String) As List(Of SP_GenerateJournalVoucherByFixedAssetReclassification_Result)

End Interface
