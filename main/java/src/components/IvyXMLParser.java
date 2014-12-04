package components;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import java.io.File;

import javax.swing.JFrame;
import javax.swing.JPanel;

import org.jdom2.Attribute;
import org.jdom2.Document;
import org.jdom2.Element;
import org.jdom2.input.SAXBuilder;

import components.IvyComponent.Alignment;
import components.IvyComponent.Border;
import components.IvyComponent.DimensionParameters;

import exception.*;

public class IvyXMLParser {
	public static IvyComponent parseXML(String fileName){
		SAXBuilder sxb = new SAXBuilder();
		Document document;
		Element racine;
		IvyComponent comp = null;
		
		try{
			document = sxb.build(new File(fileName));

			racine = document.getRootElement();

			comp = parseXMLComponent(racine);
		}catch(Exception e){
			//TODO
		}

		return comp;
	}

	public static IvyComponent parseXMLComponent(Element e) throws IvyException {
		IvyComponent comp = null;

		if(e.getName().equals("Grid")){
			comp = parseXMLGrid(e);
		}
		else if(e.getName().equals("Button")){
			comp = parseXMLButton(e);
		}
		else if(e.getName().equals("Label")){
			comp = parseXMLLabel(e);
		}
		else if(e.getName().equals("List")){
			comp = parseXMLList(e);
		}

		return comp;
	}

	//--- TEXT COMPONENT

	private static IvyButton parseXMLButton(Element e) throws IvyException {
		IvyButton button = new IvyButton();

		for(Attribute attr: e.getAttributes()){
			if(getComponentAttributes(button, attr)){
				continue;
			}

			if(getTextComponentAttributes(button, attr)){
				continue;
			}
		}

		//TODO
		return button;
	}

	private static IvyLabel parseXMLLabel(Element e) throws IvyException {
		IvyLabel label = new IvyLabel();

		for(Attribute attr: e.getAttributes()){
			if(getComponentAttributes(label, attr)){
				continue;
			}

			if(getTextComponentAttributes(label, attr)){
				continue;
			}
		}

		return label;
	}

	private static IvyGrid parseXMLGrid(Element e) throws IvyException {
		IvyGrid grid = new IvyGrid(1, 1);
		String value;
		IvyComponent compChild;

		for(Attribute attr: e.getAttributes()){
			/* Component Attributes */

			if(getComponentAttributes(grid, attr)){
				continue;
			}

			/* Grid Attributes */

			if(attr.getName().equals("rows")){
				value = attr.getValue();

				try{
					grid.setRows(Integer.parseInt(value));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
			else if(attr.getName().equals("cols")){
				value = attr.getValue();

				try{
					grid.setCols(Integer.parseInt(value));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}				
			}
		}

		for(Element child: e.getChildren()){
			compChild = parseXMLComponent(child);

			grid.setComponentAt(compChild, compChild.getRow(), compChild.getCol());
		}

		return grid;
	}

	private static IvyList parseXMLList(Element e) throws IvyException {
		IvyList list = new IvyList();

		for(Attribute attr: e.getAttributes()){
			if(getComponentAttributes(list, attr)){
				continue;
			}
		}

		return list;
	}

	private static boolean getTextComponentAttributes(IvyTextComponent comp, Attribute attr) throws IvyException {
		String value = attr.getValue();

		if(attr.getName().equals("text")){
			comp.setText(value);
		}
		else if(attr.getName().equals("textAlignment")){
			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}
			
			if(value.equals("left")){
				comp.setTextAlignment(Alignment.LEFT);
			}
			else if(value.equals("center")){
				comp.setTextAlignment(Alignment.CENTER);
			}
			else if(value.equals("right")){
				comp.setTextAlignment(Alignment.RIGHT);
			}
			else{
				throw new InvalidAttributeException();
			}
		}
		else if(attr.getName().equals("textColor")){
			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}
			
			try{
				comp.setBackgroundColor(Color.decode(value));
			}catch(Exception ex){
				throw new InvalidAttributeException();
			}
		}
		else{
			return false;
		}

		return true;
	}

	private static boolean getComponentAttributes(IvyComponent comp, Attribute attr) throws IvyException {
		String value = attr.getValue();

		if(attr.getName().equals("name")){
			//comp.setId()
		}
		else if(attr.getName().equals("width")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}

			if(value.equals("*")){
				comp.setWidthType(DimensionParameters.FILL);
			}
			else if(value.equals("auto")){
				comp.setWidthType(DimensionParameters.AUTO);
			}
			else{
				comp.setWidthType(DimensionParameters.FIXED);

				try{
					comp.setWidth(Integer.parseInt(value));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
		}
		else if(attr.getName().equals("height")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}

			if(value.equals("*")){
				comp.setHeightType(DimensionParameters.FILL);
			}
			else if(value.equals("auto")){
				comp.setHeightType(DimensionParameters.AUTO);
			}
			else{
				comp.setHeightType(DimensionParameters.FIXED);

				try{
					comp.setHeight(Integer.parseInt(value));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
		}
		else if(attr.getName().equals("margin")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}

			String[] marginValues = value.split(",");

			for(int i=0; i<marginValues.length; i++){
				marginValues[i] = marginValues[i].trim();
			}

			if(marginValues.length == 1){
				try{
					comp.setMargin(Integer.parseInt(marginValues[0]));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
			else if(marginValues.length == 2){
				try{
					comp.setMargin(Integer.parseInt(marginValues[0]), 
							Integer.parseInt(marginValues[1]));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
			else if(marginValues.length == 4){
				try{
					comp.setMargin(Integer.parseInt(marginValues[0]), 
							Integer.parseInt(marginValues[1]),
							Integer.parseInt(marginValues[2]),
							Integer.parseInt(marginValues[3]));
				}catch(Exception ex){
					throw new InvalidAttributeTypeException();
				}
			}
			else{
				throw new InvalidAttributeValueException();
			}
		}
		else if(attr.getName().equals("row")){
			try{
				comp.setRow(attr.getIntValue());
			}catch(Exception e){
				throw new InvalidAttributeTypeException();
			}
		}
		else if(attr.getName().equals("col")){
			try{
				comp.setCol(attr.getIntValue());
			}catch(Exception e){
				throw new InvalidAttributeTypeException();
			}			
		}
		else if(attr.getName().equals("rowSpan")){
			try{
				comp.setRowSpan(attr.getIntValue());
			}catch(Exception e){
				throw new InvalidAttributeTypeException();
			}
		}
		else if(attr.getName().equals("colSpan")){
			try{
				comp.setColSpan(attr.getIntValue());
			}catch(Exception e){
				throw new InvalidAttributeTypeException();
			}
		}
		else if(attr.getName().equals("border")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}

			if(value.equals("none")){
				comp.setBorderType(Border.NONE);
			}
			else if(value.equals("squared")){
				comp.setBorderType(Border.SQUARED);
			}
			else if(value.equals("rounded")){
				comp.setBorderType(Border.ROUNDED);
			}
			else{
				throw new InvalidAttributeValueException();
			}
		}
		else if(attr.getName().equals("borderColor")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}
		}
		else if(attr.getName().equals("backgroundColor")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}
			
			try{
				comp.setBackgroundColor(Color.decode(value));
			}catch(Exception ex){
				throw new InvalidAttributeException();
			}
		}
		else if(attr.getName().equals("backgroundImage")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}
		}
		else if(attr.getName().equals("visibility")){
			value = attr.getValue();

			if(value.isEmpty()){
				throw new EmptyAttributeValueException();
			}

			try{
				comp.setVisible(attr.getBooleanValue());
			}catch(Exception ex){
				throw new InvalidAttributeTypeException();
			}
		}
		else{
			return false;
		}

		return true;
	}

	public static void main(String[] args){
		javax.swing.SwingUtilities.invokeLater(new Runnable(){
			public void run(){			

				final IvyComponent comp = parseXML("test.xml");

				JFrame frame = new JFrame();
				/*JPanel content = new JPanel(){
					private static final long serialVersionUID = 1L;

					public void paintComponent(Graphics g){
						if(comp != null){
							BufferedImage bufImage = new BufferedImage(this.getPreferredSize().width,
									this.getPreferredSize().height, BufferedImage.TYPE_INT_RGB);
							//panel.paint(bufImage.createGraphics());

							//comp.draw(g, 0, 0, this.getWidth(), this.getHeight());
							comp.draw(bufImage.createGraphics(), 0, 0, this.getWidth(), this.getHeight());
							g.drawImage(bufImage, 0, 0, bufImage.getWidth(), bufImage.getHeight(), null);
							
							//comp.getComponent().addNotify();
							comp.getComponent().validate();
							//comp.getComponent().setSize(new Dimension(500, 500));
							comp.getComponent().paint(g);
						}
					}
				};*/

				frame.setSize(800, 480);

				//content.setPreferredSize(new Dimension(800, 480));

				/*frame.setContentPane(content);
				frame.pack();*/
				//content.repaint();
				frame.setContentPane(comp.getComponent());
				frame.setResizable(false);
				frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
				frame.setVisible(true);
			}
		});
	}
}
