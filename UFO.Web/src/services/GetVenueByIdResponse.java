
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
 *         &lt;element name="GetVenueByIdResult" type="{http://andipaetzold.de/}Venue" minOccurs="0"/>
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
    "getVenueByIdResult"
})
@XmlRootElement(name = "GetVenueByIdResponse")
public class GetVenueByIdResponse {

    @XmlElement(name = "GetVenueByIdResult")
    protected Venue getVenueByIdResult;

    /**
     * Gets the value of the getVenueByIdResult property.
     * 
     * @return
     *     possible object is
     *     {@link Venue }
     *     
     */
    public Venue getGetVenueByIdResult() {
        return getVenueByIdResult;
    }

    /**
     * Sets the value of the getVenueByIdResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link Venue }
     *     
     */
    public void setGetVenueByIdResult(Venue value) {
        this.getVenueByIdResult = value;
    }

}
