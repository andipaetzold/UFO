<?xml version="1.0" encoding="utf-8"?>
<wsdl:definitions xmlns:tm="http://microsoft.com/wsdl/mime/textMatching/" xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/" xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/" xmlns:tns="http://andipaetzold.de/" xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://schemas.xmlsoap.org/wsdl/soap12/" xmlns:http="http://schemas.xmlsoap.org/wsdl/http/" targetNamespace="http://andipaetzold.de/" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">
  <wsdl:types>
    <s:schema elementFormDefault="qualified" targetNamespace="http://andipaetzold.de/">
      <s:element name="GetCountryById">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="id" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetCountryByIdResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetCountryByIdResult" type="tns:Country" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="Country">
        <s:complexContent mixed="false">
          <s:extension base="tns:DatabaseObject">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Name" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:complexType name="DatabaseObject" abstract="true">
        <s:sequence>
          <s:element minOccurs="1" maxOccurs="1" name="Id" type="s:int" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetCategoryById">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="id" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetCategoryByIdResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetCategoryByIdResult" type="tns:Category" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="Category">
        <s:complexContent mixed="false">
          <s:extension base="tns:DatabaseObject">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Name" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetArtistById">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="id" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetArtistByIdResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetArtistByIdResult" type="tns:Artist" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="Artist">
        <s:complexContent mixed="false">
          <s:extension base="tns:DatabaseObject">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Category" type="tns:Category" />
              <s:element minOccurs="0" maxOccurs="1" name="Country" type="tns:Country" />
              <s:element minOccurs="0" maxOccurs="1" name="Email" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="Image" type="s:string" />
              <s:element minOccurs="1" maxOccurs="1" name="IsDeleted" type="s:boolean" />
              <s:element minOccurs="0" maxOccurs="1" name="Name" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="VideoUrl" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetVenueById">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="id" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetVenueByIdResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetVenueByIdResult" type="tns:Venue" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="Venue">
        <s:complexContent mixed="false">
          <s:extension base="tns:DatabaseObject">
            <s:sequence>
              <s:element minOccurs="1" maxOccurs="1" name="Latitude" nillable="true" type="s:decimal" />
              <s:element minOccurs="1" maxOccurs="1" name="Longitude" nillable="true" type="s:decimal" />
              <s:element minOccurs="0" maxOccurs="1" name="Name" type="s:string" />
              <s:element minOccurs="0" maxOccurs="1" name="ShortName" type="s:string" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetPerformanceById">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="id" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetPerformanceByIdResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetPerformanceByIdResult" type="tns:Performance" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="Performance">
        <s:complexContent mixed="false">
          <s:extension base="tns:DatabaseObject">
            <s:sequence>
              <s:element minOccurs="0" maxOccurs="1" name="Artist" type="tns:Artist" />
              <s:element minOccurs="1" maxOccurs="1" name="DateTime" type="s:dateTime" />
              <s:element minOccurs="0" maxOccurs="1" name="Venue" type="tns:Venue" />
            </s:sequence>
          </s:extension>
        </s:complexContent>
      </s:complexType>
      <s:element name="GetDatesWithPerformances">
        <s:complexType />
      </s:element>
      <s:element name="GetDatesWithPerformancesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetDatesWithPerformancesResult" type="tns:ArrayOfDateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfDateTime">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="dateTime" type="s:dateTime" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllPerformances">
        <s:complexType />
      </s:element>
      <s:element name="GetAllPerformancesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllPerformancesResult" type="tns:ArrayOfPerformance" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfPerformance">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Performance" nillable="true" type="tns:Performance" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllArtists">
        <s:complexType />
      </s:element>
      <s:element name="GetAllArtistsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllArtistsResult" type="tns:ArrayOfArtist" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfArtist">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Artist" nillable="true" type="tns:Artist" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllCategories">
        <s:complexType />
      </s:element>
      <s:element name="GetAllCategoriesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllCategoriesResult" type="tns:ArrayOfCategory" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfCategory">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Category" nillable="true" type="tns:Category" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllCountries">
        <s:complexType />
      </s:element>
      <s:element name="GetAllCountriesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllCountriesResult" type="tns:ArrayOfCountry" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfCountry">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Country" nillable="true" type="tns:Country" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllVenues">
        <s:complexType />
      </s:element>
      <s:element name="GetAllVenuesResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllVenuesResult" type="tns:ArrayOfVenue" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="ArrayOfVenue">
        <s:sequence>
          <s:element minOccurs="0" maxOccurs="unbounded" name="Venue" nillable="true" type="tns:Venue" />
        </s:sequence>
      </s:complexType>
      <s:element name="GetAllButDeletedArtists">
        <s:complexType />
      </s:element>
      <s:element name="GetAllButDeletedArtistsResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetAllButDeletedArtistsResult" type="tns:ArrayOfArtist" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetPerformancesByDate">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="dateTime" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetPerformancesByDateResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetPerformancesByDateResult" type="tns:ArrayOfPerformance" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetUpcomingPerformancesByArtist">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="artist" type="tns:Artist" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetUpcomingPerformancesByArtistResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetUpcomingPerformancesByArtistResult" type="tns:ArrayOfPerformance" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetPerformancesByArtist">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="artist" type="tns:Artist" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetPerformancesByArtistResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetPerformancesByArtistResult" type="tns:ArrayOfPerformance" />
          </s:sequence>
        </s:complexType>
      </s:element>
    </s:schema>
  </wsdl:types>
  <wsdl:message name="GetCountryByIdSoapIn">
    <wsdl:part name="parameters" element="tns:GetCountryById" />
  </wsdl:message>
  <wsdl:message name="GetCountryByIdSoapOut">
    <wsdl:part name="parameters" element="tns:GetCountryByIdResponse" />
  </wsdl:message>
  <wsdl:message name="GetCategoryByIdSoapIn">
    <wsdl:part name="parameters" element="tns:GetCategoryById" />
  </wsdl:message>
  <wsdl:message name="GetCategoryByIdSoapOut">
    <wsdl:part name="parameters" element="tns:GetCategoryByIdResponse" />
  </wsdl:message>
  <wsdl:message name="GetArtistByIdSoapIn">
    <wsdl:part name="parameters" element="tns:GetArtistById" />
  </wsdl:message>
  <wsdl:message name="GetArtistByIdSoapOut">
    <wsdl:part name="parameters" element="tns:GetArtistByIdResponse" />
  </wsdl:message>
  <wsdl:message name="GetVenueByIdSoapIn">
    <wsdl:part name="parameters" element="tns:GetVenueById" />
  </wsdl:message>
  <wsdl:message name="GetVenueByIdSoapOut">
    <wsdl:part name="parameters" element="tns:GetVenueByIdResponse" />
  </wsdl:message>
  <wsdl:message name="GetPerformanceByIdSoapIn">
    <wsdl:part name="parameters" element="tns:GetPerformanceById" />
  </wsdl:message>
  <wsdl:message name="GetPerformanceByIdSoapOut">
    <wsdl:part name="parameters" element="tns:GetPerformanceByIdResponse" />
  </wsdl:message>
  <wsdl:message name="GetDatesWithPerformancesSoapIn">
    <wsdl:part name="parameters" element="tns:GetDatesWithPerformances" />
  </wsdl:message>
  <wsdl:message name="GetDatesWithPerformancesSoapOut">
    <wsdl:part name="parameters" element="tns:GetDatesWithPerformancesResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllPerformancesSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllPerformances" />
  </wsdl:message>
  <wsdl:message name="GetAllPerformancesSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllPerformancesResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllArtistsSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllArtists" />
  </wsdl:message>
  <wsdl:message name="GetAllArtistsSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllArtistsResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllCategoriesSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllCategories" />
  </wsdl:message>
  <wsdl:message name="GetAllCategoriesSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllCategoriesResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllCountriesSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllCountries" />
  </wsdl:message>
  <wsdl:message name="GetAllCountriesSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllCountriesResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllVenuesSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllVenues" />
  </wsdl:message>
  <wsdl:message name="GetAllVenuesSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllVenuesResponse" />
  </wsdl:message>
  <wsdl:message name="GetAllButDeletedArtistsSoapIn">
    <wsdl:part name="parameters" element="tns:GetAllButDeletedArtists" />
  </wsdl:message>
  <wsdl:message name="GetAllButDeletedArtistsSoapOut">
    <wsdl:part name="parameters" element="tns:GetAllButDeletedArtistsResponse" />
  </wsdl:message>
  <wsdl:message name="GetPerformancesByDateSoapIn">
    <wsdl:part name="parameters" element="tns:GetPerformancesByDate" />
  </wsdl:message>
  <wsdl:message name="GetPerformancesByDateSoapOut">
    <wsdl:part name="parameters" element="tns:GetPerformancesByDateResponse" />
  </wsdl:message>
  <wsdl:message name="GetUpcomingPerformancesByArtistSoapIn">
    <wsdl:part name="parameters" element="tns:GetUpcomingPerformancesByArtist" />
  </wsdl:message>
  <wsdl:message name="GetUpcomingPerformancesByArtistSoapOut">
    <wsdl:part name="parameters" element="tns:GetUpcomingPerformancesByArtistResponse" />
  </wsdl:message>
  <wsdl:message name="GetPerformancesByArtistSoapIn">
    <wsdl:part name="parameters" element="tns:GetPerformancesByArtist" />
  </wsdl:message>
  <wsdl:message name="GetPerformancesByArtistSoapOut">
    <wsdl:part name="parameters" element="tns:GetPerformancesByArtistResponse" />
  </wsdl:message>
  <wsdl:portType name="UltimateFestivalOrganizerSoap">
    <wsdl:operation name="GetCountryById">
      <wsdl:input message="tns:GetCountryByIdSoapIn" />
      <wsdl:output message="tns:GetCountryByIdSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetCategoryById">
      <wsdl:input message="tns:GetCategoryByIdSoapIn" />
      <wsdl:output message="tns:GetCategoryByIdSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetArtistById">
      <wsdl:input message="tns:GetArtistByIdSoapIn" />
      <wsdl:output message="tns:GetArtistByIdSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetVenueById">
      <wsdl:input message="tns:GetVenueByIdSoapIn" />
      <wsdl:output message="tns:GetVenueByIdSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetPerformanceById">
      <wsdl:input message="tns:GetPerformanceByIdSoapIn" />
      <wsdl:output message="tns:GetPerformanceByIdSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetDatesWithPerformances">
      <wsdl:input message="tns:GetDatesWithPerformancesSoapIn" />
      <wsdl:output message="tns:GetDatesWithPerformancesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllPerformances">
      <wsdl:input message="tns:GetAllPerformancesSoapIn" />
      <wsdl:output message="tns:GetAllPerformancesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllArtists">
      <wsdl:input message="tns:GetAllArtistsSoapIn" />
      <wsdl:output message="tns:GetAllArtistsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllCategories">
      <wsdl:input message="tns:GetAllCategoriesSoapIn" />
      <wsdl:output message="tns:GetAllCategoriesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllCountries">
      <wsdl:input message="tns:GetAllCountriesSoapIn" />
      <wsdl:output message="tns:GetAllCountriesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllVenues">
      <wsdl:input message="tns:GetAllVenuesSoapIn" />
      <wsdl:output message="tns:GetAllVenuesSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetAllButDeletedArtists">
      <wsdl:input message="tns:GetAllButDeletedArtistsSoapIn" />
      <wsdl:output message="tns:GetAllButDeletedArtistsSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByDate">
      <wsdl:input message="tns:GetPerformancesByDateSoapIn" />
      <wsdl:output message="tns:GetPerformancesByDateSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetUpcomingPerformancesByArtist">
      <wsdl:input message="tns:GetUpcomingPerformancesByArtistSoapIn" />
      <wsdl:output message="tns:GetUpcomingPerformancesByArtistSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByArtist">
      <wsdl:input message="tns:GetPerformancesByArtistSoapIn" />
      <wsdl:output message="tns:GetPerformancesByArtistSoapOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:binding name="UltimateFestivalOrganizerSoap" type="tns:UltimateFestivalOrganizerSoap">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="GetCountryById">
      <soap:operation soapAction="http://andipaetzold.de/GetCountryById" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetCategoryById">
      <soap:operation soapAction="http://andipaetzold.de/GetCategoryById" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetArtistById">
      <soap:operation soapAction="http://andipaetzold.de/GetArtistById" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetVenueById">
      <soap:operation soapAction="http://andipaetzold.de/GetVenueById" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformanceById">
      <soap:operation soapAction="http://andipaetzold.de/GetPerformanceById" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetDatesWithPerformances">
      <soap:operation soapAction="http://andipaetzold.de/GetDatesWithPerformances" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllPerformances">
      <soap:operation soapAction="http://andipaetzold.de/GetAllPerformances" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllArtists">
      <soap:operation soapAction="http://andipaetzold.de/GetAllArtists" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllCategories">
      <soap:operation soapAction="http://andipaetzold.de/GetAllCategories" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllCountries">
      <soap:operation soapAction="http://andipaetzold.de/GetAllCountries" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllVenues">
      <soap:operation soapAction="http://andipaetzold.de/GetAllVenues" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllButDeletedArtists">
      <soap:operation soapAction="http://andipaetzold.de/GetAllButDeletedArtists" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByDate">
      <soap:operation soapAction="http://andipaetzold.de/GetPerformancesByDate" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetUpcomingPerformancesByArtist">
      <soap:operation soapAction="http://andipaetzold.de/GetUpcomingPerformancesByArtist" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByArtist">
      <soap:operation soapAction="http://andipaetzold.de/GetPerformancesByArtist" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="UltimateFestivalOrganizerSoap12" type="tns:UltimateFestivalOrganizerSoap">
    <soap12:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="GetCountryById">
      <soap12:operation soapAction="http://andipaetzold.de/GetCountryById" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetCategoryById">
      <soap12:operation soapAction="http://andipaetzold.de/GetCategoryById" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetArtistById">
      <soap12:operation soapAction="http://andipaetzold.de/GetArtistById" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetVenueById">
      <soap12:operation soapAction="http://andipaetzold.de/GetVenueById" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformanceById">
      <soap12:operation soapAction="http://andipaetzold.de/GetPerformanceById" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetDatesWithPerformances">
      <soap12:operation soapAction="http://andipaetzold.de/GetDatesWithPerformances" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllPerformances">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllPerformances" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllArtists">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllArtists" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllCategories">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllCategories" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllCountries">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllCountries" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllVenues">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllVenues" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetAllButDeletedArtists">
      <soap12:operation soapAction="http://andipaetzold.de/GetAllButDeletedArtists" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByDate">
      <soap12:operation soapAction="http://andipaetzold.de/GetPerformancesByDate" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetUpcomingPerformancesByArtist">
      <soap12:operation soapAction="http://andipaetzold.de/GetUpcomingPerformancesByArtist" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="GetPerformancesByArtist">
      <soap12:operation soapAction="http://andipaetzold.de/GetPerformancesByArtist" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:service name="UltimateFestivalOrganizer">
    <wsdl:port name="UltimateFestivalOrganizerSoap" binding="tns:UltimateFestivalOrganizerSoap">
      <soap:address location="http://localhost:13643/UltimateFestivalOrganizer.asmx" />
    </wsdl:port>
    <wsdl:port name="UltimateFestivalOrganizerSoap12" binding="tns:UltimateFestivalOrganizerSoap12">
      <soap12:address location="http://localhost:13643/UltimateFestivalOrganizer.asmx" />
    </wsdl:port>
  </wsdl:service>
</wsdl:definitions>