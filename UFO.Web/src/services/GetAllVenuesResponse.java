
package services;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="GetAllVenuesResult" type="{http://andipaetzold.de/}ArrayOfVenue" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "getAllVenuesResult"
})
@XmlRootElement(name = "GetAllVenuesResponse")
public class GetAllVenuesResponse {

    @XmlElement(name = "GetAllVenuesResult")
    protected ArrayOfVenue getAllVenuesResult;

    /**
     * Gets the value of the getAllVenuesResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfVenue }
     *     
     */
    public ArrayOfVenue getGetAllVenuesResult() {
        return getAllVenuesResult;
    }

    /**
     * Sets the value of the getAllVenuesResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfVenue }
     *     
     */
    public void setGetAllVenuesResult(ArrayOfVenue value) {
        this.getAllVenuesResult = value;
    }

}
