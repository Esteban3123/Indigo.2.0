'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class HCREGEGRERepository
    Inherits GenericRepository(Of HCREGEGRE)
    Implements IHCREGEGRERepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    Public Function GetHCREGEGREByAdmissionCode(admissionCode As String) As HCREGEGRE Implements IHCREGEGRERepository.GetHCREGEGREByAdmissionCode
        Dim query = (From e In _crystalContext.HCREGEGRE.AsNoTracking().Include("INUNIFUNC").AsNoTracking() Where e.NUMINGRES.Equals(admissionCode) Select e).FirstOrDefault()
        If query IsNot Nothing Then

            Dim unifunc = (From u In _crystalContext.INUNIFUNC.AsNoTracking() Where u.UFUCODIGO.Equals(query.INUNIFUNC.UFUCODIGO) Select New With {.Codigo = u.UFUCODIGO, .Nombre = u.UFUDESCRI}).FirstOrDefault()
            query.FunctionalUnitFullName = String.Concat(unifunc.Codigo.Trim(), " - ", unifunc.Nombre.Trim())

            Return query
        Else
            Return Nothing
        End If
    End Function

End Class
