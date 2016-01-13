
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
 *         &lt;element name="GetCountryByIdResult" type="{http://andipaetzold.de/}Country" minOccurs="0"/>
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
    "getCountryByIdResult"
})
@XmlRootElement(name = "GetCountryByIdResponse")
public class GetCountryByIdResponse {

    @XmlElement(name = "GetCountryByIdResult")
    protected Country getCountryByIdResult;

    /**
     * Gets the value of the getCountryByIdResult property.
     * 
     * @return
     *     possible object is
     *     {@link Country }
     *     
     */
    public Country getGetCountryByIdResult() {
        return getCountryByIdResult;
    }

    /**
     * Sets the value of the getCountryByIdResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link Country }
     *     
     */
    public void setGetCountryByIdResult(Country value) {
        this.getCountryByIdResult = value;
    }

}
