
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
 *         &lt;element name="GetPerformancesByDateResult" type="{http://andipaetzold.de/}ArrayOfPerformance" minOccurs="0"/>
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
    "getPerformancesByDateResult"
})
@XmlRootElement(name = "GetPerformancesByDateResponse")
public class GetPerformancesByDateResponse {

    @XmlElement(name = "GetPerformancesByDateResult")
    protected ArrayOfPerformance getPerformancesByDateResult;

    /**
     * Gets the value of the getPerformancesByDateResult property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public ArrayOfPerformance getGetPerformancesByDateResult() {
        return getPerformancesByDateResult;
    }

    /**
     * Sets the value of the getPerformancesByDateResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfPerformance }
     *     
     */
    public void setGetPerformancesByDateResult(ArrayOfPerformance value) {
        this.getPerformancesByDateResult = value;
    }

}
