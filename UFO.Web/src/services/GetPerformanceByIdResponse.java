
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
 *         &lt;element name="GetPerformanceByIdResult" type="{http://andipaetzold.de/}Performance" minOccurs="0"/>
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
    "getPerformanceByIdResult"
})
@XmlRootElement(name = "GetPerformanceByIdResponse")
public class GetPerformanceByIdResponse {

    @XmlElement(name = "GetPerformanceByIdResult")
    protected Performance getPerformanceByIdResult;

    /**
     * Gets the value of the getPerformanceByIdResult property.
     * 
     * @return
     *     possible object is
     *     {@link Performance }
     *     
     */
    public Performance getGetPerformanceByIdResult() {
        return getPerformanceByIdResult;
    }

    /**
     * Sets the value of the getPerformanceByIdResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link Performance }
     *     
     */
    public void setGetPerformanceByIdResult(Performance value) {
        this.getPerformanceByIdResult = value;
    }

}
